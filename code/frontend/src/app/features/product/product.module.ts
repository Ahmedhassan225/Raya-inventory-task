import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { ProductRoutingModule } from './product-routing.module';
import { SharedModule } from 'src/app/shared/shared.module';
import { ProductService } from './utils';
import { ProductListPage } from './pages/product-list/product-list.page';
import { ProductViewComponent, TransactionDialogComponent } from './components';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';


@NgModule({
  declarations: [
    ProductListPage,
    ProductViewComponent,
    TransactionDialogComponent
    ],
  imports: [
    CommonModule,
    ProductRoutingModule,
    SharedModule
  ],
  providers:[ProductService]
})
export class ProductModule { }
