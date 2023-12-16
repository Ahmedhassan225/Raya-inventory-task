import { DIALOG_DATA } from '@angular/cdk/dialog';
import { Component, Inject, OnInit } from '@angular/core';
import { MatDialogRef } from '@angular/material/dialog';
import { ToastrService } from 'ngx-toastr';
import { ProductService, addProductRequest } from '../../utils';
import { ApiBaseContentModel, ApiBaseErrorModel, KVModel, dialogMode } from 'src/app/shared';

@Component({
  selector: 'app-product-view',
  templateUrl: './product-view.component.html',
  styleUrls: ['./product-view.component.css']
})
export class ProductViewComponent implements OnInit {
  
  model: addProductRequest = new addProductRequest();
  constructor(public dialogRef: MatDialogRef<ProductViewComponent>,
    @Inject(DIALOG_DATA) public data: {productId: number | null, categories: KVModel[], mode: dialogMode}
    ,private service: ProductService
    ,private toastr: ToastrService) { }
  
  ngOnInit(): void {
    if(this.data.productId && this.data.mode != dialogMode.Add)
    {
      this.service.getProduct(this.data.productId).subscribe(result => {
        this.model.productId = result.result.id;
        this.model.productCode = result.result.code;
        this.model.productName = result.result.name;
        this.model.ProductDescription = result.result.description;
        this.model.productQuantity = result.result.quantity;
        this.model.productPrice = result.result.price;
        this.model.productCategoryId = result.result.categoryId;

      })
    }    
  }

  save(){
    
    switch (this.data.mode) {
      case dialogMode.Add:
        this.service.createProduct(this.model).subscribe({
          next: value => {
            if (value.result === true) {
              this.handleNext(value);
            } else {
              this.toastr.error('Product Creation failed');
            }
          },
          error: err => {
            console.error('Observable emitted an error: ' + err);
            this.handleError(err)        
          },
          complete: () => {
            console.log('Observable emitted the complete notification')
            this.dialogRef.close(true);
          }
        });
        break;

      case dialogMode.Edit:
        this.service.updateProduct(this.model).subscribe({
          next: value => {
            if (value.result === true) {
              this.handleNext(value);
            } else {
              this.toastr.error('Product Update failed');
            }
          },
          error: err => {
            console.error('Observable emitted an error: ' + err);
            this.handleError(err)        
          },
          complete: () => {
            console.log('Observable emitted the complete notification');
            this.dialogRef.close(true);
          }
        });
        break;

      default:
        break;
    }
}

  handleNext(result: ApiBaseContentModel<boolean>){
    let message = 'Saved';
    if(result.result)
      this.toastr.success(message)  ;    

  }

  handleError(error: any){
    var customError: ApiBaseErrorModel = error.error;
    if(customError)
    {
      customError.Messages.forEach(message => {
        this.toastr.error(message)  ;    
      });
    }
  }

}
