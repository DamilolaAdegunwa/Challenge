import { NativeDateAdapter } from "@angular/material";

export const MY_DATE_FORMATS = {
    parse: {
        dateInput: { month: 'short', year: 'numeric', day: 'numeric' },
    },
    display: {
        dateInput: 'input',
        monthYearLabel: { month: 'numeric', year: 'numeric', },
        dateA11yLabel: { day: 'numeric', month: 'long', year: 'numeric' },
        monthYearA11yLabel: { month: 'long', year: 'numeric' },
    },
};

export const DD_MM_YYYY_Format = {
	parse: {
		dateInput: 'D/MM/YYYY',
	},
	display: {
		dateInput: 'DD/MM/YYYY',
		monthYearLabel: 'MMM YYYY',
		dateA11yLabel: 'LL',
		monthYearA11yLabel: 'MMMM YYYY',
	},
};

export class CustomDateAdapter extends NativeDateAdapter {

	parse(value: any): Date | null {

		if ((typeof value === 'string') && (value.indexOf('/') > -1)) {
			const str = value.split('/');

			const year = Number(str[2]);
			const month = Number(str[1]) - 1;
			const date = Number(str[0]);

			return new Date(year, month, date);
		}
		const timestamp = typeof value === 'number' ? value : Date.parse(value);
		return isNaN(timestamp) ? null : new Date(timestamp);
	}

	// retirar quando for feito o merge da data por mmalerba
	format(date: Date, displayFormat: Object): string {
		date = new Date(Date.UTC(
			date.getFullYear(), date.getMonth(), date.getDate(), date.getHours(),
			date.getMinutes(), date.getSeconds(), date.getMilliseconds()));
		displayFormat = Object.assign({}, displayFormat, { timeZone: 'utc' });


		const dtf = new Intl.DateTimeFormat(this.locale, displayFormat);
		return dtf.format(date).replace(/[\u200e\u200f]/g, '');
	}

}
