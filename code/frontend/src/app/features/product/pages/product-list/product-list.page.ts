import { AfterViewInit, Component, OnInit, ViewChild } from '@angular/core';
import { NgForm } from '@angular/forms';
import { MatPaginator, PageEvent } from '@angular/material/paginator';
import { MatTableDataSource } from '@angular/material/table';
import { ProductViewComponent } from '../../components/product-view/product-view.component';
import {
  MatDialog
} from '@angular/material/dialog';
import { ToastrService } from 'ngx-toastr';
import { ConfirmDialogComponent } from '../../../../shared/components/confirm-dialog/confirm-dialog.component';
import { TransactionDialogComponent } from '../../components/transaction-dialog/transaction-dialog.component';
import { ProductService, getProductsRequest, getProductsResponce } from '../../utils';
import { ConfirmDialogModel, KVModel, LookupsService, dialogMode, productOrderBy } from 'src/app/shared';


@Component({
  selector: 'app-product-list',
  templateUrl: './product-list.page.html',
  styleUrls: ['./product-list.page.css']
})
export class ProductListPage implements OnInit {


  displayedColumns: string[] = ['code', 'name','category', 'quantaty', 'price', 'actions'];

  isLoadingResults = false;
  @ViewChild(MatPaginator) paginator!: MatPaginator;

  searchModel: getProductsRequest = {
    pageIndex: 1,
    pageSize: 5
  };

  orderSelectModel: KVModel[] = []
  categorySelectModel: KVModel[] = []
  totalRows = 0;
  pageSize = 5;
  currentPage = 0;
  pageSizeOptions: number[] = [5, 10, 25, 100];
  dataSource: MatTableDataSource<getProductsResponce> = new MatTableDataSource();

  orderModel = productOrderBy

  constructor(private service: ProductService, private lookupService: LookupsService, private dialog: MatDialog,private toastr: ToastrService) { }

  ngOnInit(): void {
    this.list();

    this.orderSelectModel = Object.keys(productOrderBy).filter(k => !isNaN(Number(k))).map((e: any) => {
      return { id: Number(e), name: this.orderModel[e] } as KVModel
    });;

    this.lookupService.getCategory().subscribe((next) => {
      this.categorySelectModel = next.result;
    })
  }

  list(){
    this.isLoadingResults = true;

    this.searchModel.pageIndex = this.currentPage;
    this.searchModel.pageSize = this.pageSize;

    this.service.getAll(this.searchModel).subscribe(
      (result) => {
        this.dataSource.data = result.result;
        setTimeout(() => {
          this.paginator.pageIndex = this.currentPage;
          this.paginator.length = result.pageInfo.totalCount;
        });

        this.isLoadingResults = false;
      }
    ,(error) =>{
      this.isLoadingResults = false;
      console.log(error.error);
      
    })

    return this.dataSource;
  }

  confirmDelete(id: number){
    const message = `Are you sure you want to delete this?`;

    const dialogData = new ConfirmDialogModel("Confirm Action", message);

    const dialogRef = this.dialog.open(ConfirmDialogComponent, {
      maxWidth: "400px",
      data: dialogData
    });

    dialogRef.afterClosed().subscribe(dialogResult => {
      if(dialogResult){
        this.service.deleteProduct(id).subscribe({
          next: value => {
            if (value.result === true) {
              this.toastr.success('Product deleted successfully');
              this.list();
            } else {
              this.toastr.error('Product deletion failed');
            }
          },
          error: err => {
            console.error('Observable emitted an error: ' + err);
            this.toastr.error('Error deleting product');
          },
          complete: () => console.log('Observable emitted the complete notification')
        });
      }
    });
  }

  search(){
    this.currentPage = 0;
    this.list();
  }

  pageChanged(event: PageEvent) {
    console.log({ event });
  
    this.pageSize = event.pageSize;
    this.currentPage = event.pageIndex;
    this.list();
  }

  openProductDialog(element: getProductsResponce | null, mode : dialogMode): void {
    const dialogRef = this.dialog.open(ProductViewComponent, {
      data: {productId: element?.productId, categories: this.categorySelectModel, mode: mode},
      height: '70vh',
      width: '30vw'
    });

    dialogRef.afterClosed().subscribe(result => {
      if(result)
        this.list();
    });
  }

  openTransactionDialog(productId: number){
    const dialogRef = this.dialog.open(TransactionDialogComponent, {
      data: {productId: productId},
     
    });

    dialogRef.afterClosed().subscribe(result => {
      if(result)
        this.list();
    });
  }
 
  clear(){
    this.toastr.success('Hello world!', 'Toastr fun!');
    this.searchModel.productCode = undefined;
    this.searchModel.productName = undefined;
    this.searchModel.categoryId = undefined;
    this.searchModel.orderBy = undefined;
    this.list();
  }
}

