import { FormRules } from "element-plus"
import { Ref, ref } from "vue"


export interface SimpleFormModel {
    type: 'input' | 'number' | 'select' | 'checkbox' | 'datetime' | 'date'
    name: string
    label: string,
    value: any
    placeholder?: string,
    dateTimeOption?: {
        type: 'year' | 'month' | 'date' | 'datetime' | 'week' | 'datetimerange' | 'daterange',
        format?:string
    }
}
