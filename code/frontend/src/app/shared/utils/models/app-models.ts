export interface ApiBaseContentModel<T>{
    result: T;
    pageInfo: PageInfoAppModel
}

export interface ApiBaseErrorModel{
    Messages: string [];
    source: string;
    exception:string;
    errorId: string;
    supportMessage: string;
    statusCode: number;
}

export class KVModel {
    id?: number = undefined;
    name?: string = undefined;
}

export class PageInfoAppModel {
    currentPage?= 0;
    totalCount = 0;
    pageSize = 10;
    get totalPages(): number {
        return this.totalCount / this.pageSize
    }

    constructor(pageSize?: number, totalCount?: number) {
        this.pageSize = pageSize || 10;
        this.totalCount = totalCount || 0;
    }

    update(pageInfo: PageInfoAppModel) {

        this.currentPage = pageInfo.currentPage;
        this.totalCount = pageInfo.totalCount;
        this.pageSize = pageInfo.pageSize;
    }
}

export class ConfirmDialogModel {

    constructor(public title: string, public message: string) {
    }
}


//#region enums 
export enum dialogMode {
    View,
    Add,
    Edit
}

export enum productOrderBy{
    Price = 1,
    Quantity = 2,
    Name = 3,
}

export enum transactionType {
    Purchase = 1,
    Sale = 2,
}

//#endregion    