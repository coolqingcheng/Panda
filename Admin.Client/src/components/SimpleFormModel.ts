import { FormRules } from "element-plus"
import { Ref, ref } from "vue"


export interface SimpleFormModel {
    type: 'input' | 'number' | 'select' | 'checkbox' | 'datetime' | 'date' | 'switch'
    name: string
    label: string,
    value?: any
    placeholder?: string,
    dateTimeOption?: {
        type: 'year' | 'month' | 'date' | 'datetime' | 'week' | 'datetimerange' | 'daterange',
        format?: string
    },
    selectOption?: {
        items: { label: string, value: any }[]
    },
    inputOption?: {
        isNumber: false
    }
}
