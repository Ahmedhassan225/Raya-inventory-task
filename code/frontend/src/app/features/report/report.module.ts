import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { ReportRoutingModule } from './report-routing.module';
import { SharedModule } from 'src/app/shared/shared.module';
import { ProductReportPage } from './pages/product-report/product-report.page';
import { ReportingService } from './utils/services/reporting.service';


@NgModule({
  declarations: [
    ProductReportPage,
  ],
  imports: [
    CommonModule,
    ReportRoutingModule,
    SharedModule
  ],
  providers: [ReportingService]
})
export class ReportModule { }
