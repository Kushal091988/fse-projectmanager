import * as _ from 'lodash';

class Common {

    isNil(value: any): boolean {
        return _.isNil(value);
    }

    YYYYMMDDToDate(dateStr: string): Date {
        const year = dateStr.substring(0, 4);
        const month = dateStr.substring(4, 6);
        const day = dateStr.substring(6, 8);

        return new Date(+year, +month - 1, +day);
    }

    dateToYYYYMMDD(value: Date): string {
        const y = value.getFullYear();
        const m = value.getMonth() + 1;
        const d = value.getDate();
        return '' + y + (m < 10 ? '0' : '') + m + (d < 10 ? '0' : '') + d;
    }

    isArray(obj: any): boolean {
        return _.isArray(obj);
    }

    isNumber(obj: any): boolean {
        return _.isNumber(obj);
    }

    isString(obj: any): boolean {
        return _.isString(obj);
    }

    /** Object deep clone */
    cloneDeep(value: any) {
        return _.cloneDeep(value);
    }

    /** JSON stringify clone */
    clone<T>(obj: T): T {
        return <T>JSON.parse(JSON.stringify(obj));
    }
}

export const common = new Common();
