export interface getProductsResponce {
    productId: number;
    productCode: string;
    productName: string;
    productPrice: number;
    productQuantaty: number;
    categoryName: string;

}

export class getProductsRequest {
    productCode?: string = undefined;
    productName?: string = undefined;
    categoryId?: number = undefined;
    orderBy?: number = undefined;
    pageIndex: number = 1;
    pageSize: number = 5;

    [key: string]: any; // Index signature for dynamic properties
}

export class addProductRequest {
    productId?: number = undefined;
    productName?: string = undefined;
    ProductDescription?: string = undefined;
    productCode?: string = undefined; 
    productCategoryId?: number = undefined;
    productPrice: number = 0;
    productQuantity: number = 0;
}

export class getProductByIdResponce {
    id?: number = undefined;
    name?: string = undefined;
    description?: string = undefined;
    code?: string = undefined; 
    categoryId ?: number = undefined;
    quantity: number = 0;
    price: number = 0;
}

export class addTransaction 
{
    productId?: number = undefined;
    quantity?: number = undefined;
    type: number = 1;
}


