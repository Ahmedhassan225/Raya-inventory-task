import { Component, Inject, OnInit } from '@angular/core';
import { MAT_DIALOG_DATA, MatDialogRef } from '@angular/material/dialog';
import { ToastrService } from 'ngx-toastr';
import { ProductService, addTransaction } from '../../utils';
import { ApiBaseContentModel, ApiBaseErrorModel, KVModel, transactionType } from 'src/app/shared';

@Component({
  selector: 'app-transaction-dialog',
  templateUrl: './transaction-dialog.component.html',
  styleUrls: ['./transaction-dialog.component.css']
})
export class TransactionDialogComponent implements OnInit {

  constructor(private service: ProductService, public dialogRef:MatDialogRef<TransactionDialogComponent>, @Inject(MAT_DIALOG_DATA) public data: {productId: number}, private toastr: ToastrService) { }
  
  transactionModel = transactionType
  transactionTypes: KVModel[] = []

  model: addTransaction = new addTransaction();
  ngOnInit(): void {
    this.model.productId = this.data.productId;

    this.transactionTypes = Object.keys(transactionType).filter(k => !isNaN(Number(k))).map((e: any) => {
      return { id: Number(e), name: this.transactionModel[e] } as KVModel
    });;
  }

  onConfirm(){
    this.model.type = Number(this.model.type);
    this.service.createtransaction(this.model).subscribe({
      next: value => {
        if (value.result === true) {
          this.handleNext(value);
        } else {
          this.toastr.error('Transaction Creation failed');
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
