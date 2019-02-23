using FWK.Domain;
using FWK.Domain.Auditing;
using FWK.Domain.bus;
using FWK.Domain.Entities;
using FWK.Domain.Event;
using FWK.Domain.Interfaces.Entities;
using FWK.Domain.Interfaces.Services;
using FWK.Domain.UOW;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace FWK.Infra.Data
{
    public class BaseContext : DbContext
    {

        public BaseContext(DbContextOptions options)
            : base(options)
        {
            this.InitializeDbContext();
        }


        public override void Dispose()
        {
            base.Dispose();
        }

        private void InitializeDbContext()
        {
           
        }
         
        protected IAuthService authService { get; set; } 

        public override Task<int> SaveChangesAsync(bool acceptAllChangesOnSuccess, CancellationToken cancellationToken = default(CancellationToken))
        {
            return base.SaveChangesAsync(acceptAllChangesOnSuccess, cancellationToken);
        }
        public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default(CancellationToken))
        {
            EntityChangeReport entityChangeReport = this.ApplyAbpConcepts();

            var result = await base.SaveChangesAsync(cancellationToken);

            SaveLogs(entityChangeReport);

            await base.SaveChangesAsync();

            return result;
        }

        public override int SaveChanges(bool acceptAllChangesOnSuccess)
        {
            EntityChangeReport entityChangeReport = this.ApplyAbpConcepts();

            var result = base.SaveChanges(acceptAllChangesOnSuccess);

            SaveLogs(entityChangeReport);

            base.SaveChanges(acceptAllChangesOnSuccess);

            return result;
        }

        public void SaveLogs(EntityChangeReport entityChangeReport)
        {
            //LOGS
            foreach (var entry in entityChangeReport.ChangedEntities)
            {
                //TODO: AZ implementar;
            }
        }

        public override int SaveChanges()
        {

            try
            {
                EntityChangeReport entityChangeReport = this.ApplyAbpConcepts();
                int num = base.SaveChanges();
                //todo falta ver los eventos
                //this.EntityChangeEventHelper.TriggerEvents(entityChangeReport);

                return num;
            }
            catch (DbUpdateConcurrencyException ex)
            {
                throw new DBConcurrencyException(ex.Message, (Exception)ex);
            }
        }

        protected virtual EntityChangeReport ApplyAbpConcepts()
        {
            EntityChangeReport changeReport = new EntityChangeReport();
            int? auditUserId = this.GetAuditUserId();
            foreach (EntityEntry entry in this.ChangeTracker.Entries().ToList<EntityEntry>())
            {
                this.ApplyAbpConcepts(entry, auditUserId, changeReport);
            }

            return changeReport;
        }


        protected virtual void ApplyAbpConcepts(EntityEntry entry, int? userId, EntityChangeReport changeReport)
        {
            switch (entry.State)
            {
                case EntityState.Deleted:
                    this.ApplyAbpConceptsForDeletedEntity(entry, userId, changeReport);
                    break;
                case EntityState.Modified:
                    
                    this.ApplyAbpConceptsForModifiedEntity(entry, userId, changeReport);
                    break;
                case EntityState.Added:
          
                    this.ApplyAbpConceptsForAddedEntity(entry, userId, changeReport);
                    break;
            }

            this.AddDomainEvents(changeReport.DomainEvents, entry.Entity);
        }

    

        protected virtual void AddDomainEvents(List<DomainEventEntry> domainEvents, object entityAsObj)
        {
            IGeneratesDomainEvents igeneratesDomainEvents = entityAsObj as IGeneratesDomainEvents;
            if (igeneratesDomainEvents == null || FWK.Extensions.CollectionExtensions.IsNullOrEmpty<IEventData>((ICollection<IEventData>)igeneratesDomainEvents.DomainEvents))
                return;
            domainEvents.AddRange(((IEnumerable<IEventData>)igeneratesDomainEvents.DomainEvents).Select<IEventData, DomainEventEntry>((Func<IEventData, DomainEventEntry>)(eventData => new DomainEventEntry(entityAsObj, eventData))));
            igeneratesDomainEvents.DomainEvents.Clear();
        }



        protected virtual void SetCreationAuditProperties(object entityAsObj, int? userId)
        {
            EntityAuditingHelper.SetCreationAuditProperties(entityAsObj, userId);
        }

        protected virtual void SetModificationAuditProperties(object entityAsObj, int? userId)
        {
            EntityAuditingHelper.SetModificationAuditProperties(entityAsObj, userId);
        }

        protected virtual void SetDeletionAuditProperties(object entityAsObj, int? userId)
        {
            EntityAuditingHelper.SetDeletionAuditProperties(entityAsObj, userId);
        }


        protected virtual void ApplyAbpConceptsForAddedEntity(EntityEntry entry, int? userId, EntityChangeReport changeReport)
        {
            this.CheckAndSetId(entry);

            this.SetCreationAuditProperties(entry.Entity, userId);
            changeReport.ChangedEntities.Add(new EntityChangeEntry(entry.Entity, (EntityChangeType)0));
        }

        protected virtual void ApplyAbpConceptsForModifiedEntity(EntityEntry entry, int? userId, EntityChangeReport changeReport)
        {
            this.SetModificationAuditProperties(entry.Entity, userId);
            if (entry.Entity is ISoftDelete && ((ISoftDelete)ObjectExtensions.As<ISoftDelete>(entry.Entity)).IsDeleted)
            {
                this.SetDeletionAuditProperties(entry.Entity, userId);
                changeReport.ChangedEntities.Add(new EntityChangeEntry(entry.Entity, (EntityChangeType)2));
            }
            else
                changeReport.ChangedEntities.Add(new EntityChangeEntry(entry.Entity, (EntityChangeType)1));
        }

        protected virtual void ApplyAbpConceptsForDeletedEntity(EntityEntry entry, int? userId, EntityChangeReport changeReport)
        {
            this.CancelDeletionForSoftDelete(entry);
            this.SetDeletionAuditProperties(entry.Entity, userId);
            changeReport.ChangedEntities.Add(new EntityChangeEntry(entry.Entity, (EntityChangeType)2));
        }


        protected virtual void CancelDeletionForSoftDelete(EntityEntry entry)
        {
            if (!(entry.Entity is ISoftDelete))
                return;
            entry.Reload();
            entry.State = EntityState.Modified;
            ((ISoftDelete)ObjectExtensions.As<ISoftDelete>(entry.Entity)).IsDeleted = true;
        }


        protected virtual void CheckAndSetId(EntityEntry entry)
        {
            //IEntity<Guid> entity = entry.Entity as IEntity<Guid>;
            //if (entity == null || !(entity.Id == Guid.Empty))
            //    return;
            //DatabaseGeneratedAttribute attributeOrDefault = (DatabaseGeneratedAttribute)ReflectionHelper.GetSingleAttributeOrDefault<DatabaseGeneratedAttribute>((MemberInfo)entry.Property("Id").Metadata.PropertyInfo, (M0)null, true);
            //if (attributeOrDefault != null && attributeOrDefault.DatabaseGeneratedOption != DatabaseGeneratedOption.None)
            //    return;
            //entity.set_Id(this.GuidGenerator.Create());
        }

        protected int? AuditUserId { get; set; }
        protected int CountryId { get; set; }

        public virtual int? GetAuditUserId()
        {
            //sacar id de la sesscion?
            return AuditUserId ?? (AuditUserId = authService?.GetCurretUserId());
        }

        public virtual int GetCurretCountryId()
        {
            return CountryId == 0 ? (CountryId = authService.GetCurretCountryId()) : CountryId;
        }

    }
}
