import { NzTableQueryParams } from "ng-zorro-antd/table";

export class BaseTableComponent {

   private _getDataFunc?: Function;

    constructor(getDataFunc:Function) {
        this._getDataFunc = getDataFunc;
    }

    page = 1

    size = 10

    total = 0;

    loading = false;
    tableChange(params: NzTableQueryParams) {
        const { pageIndex, pageSize } = params;
        this.page = pageIndex
        this.size = pageSize;
        if (this._getDataFunc) {
            this._getDataFunc()
        }
    }
}