import { Component, OnInit } from '@angular/core';
import { ReportingService } from '../../utils/services/reporting.service';
import { DomSanitizer, SafeUrl } from '@angular/platform-browser';
import { KVModel, LookupsService, productOrderBy } from 'src/app/shared';
import { ProductReportRequest } from '../../utils/models/app-models';


@Component({
  selector: 'app-product-report',
  templateUrl: './product-report.page.html',
  styleUrls: ['./product-report.page.css']
})
export class ProductReportPage implements OnInit {
  isLoading = false
  pdfPreviewURL?: any = undefined
  categorySelectModel: KVModel[] = []

  searchModel: ProductReportRequest = new ProductReportRequest();

  orderSelectModel: KVModel[] = []
  orderModel = productOrderBy

  constructor(private service: ReportingService, private lookupSerive: LookupsService, private sanitizer: DomSanitizer) { }

  ngOnInit(): void {
    this.orderSelectModel = Object.keys(productOrderBy).filter(k => !isNaN(Number(k))).map((e: any) => {
      return { id: Number(e), name: this.orderModel[e] } as KVModel
    });;

    this.loadCategories();
  }

  LoadReport(){
    this.isLoading = true;
    this.pdfPreviewURL = undefined
    this.service.getReport(this.searchModel).subscribe({
      next: (htmlData: any) => {
      
        // Handle the PDF data as needed
        const blob = new Blob([htmlData], { type: 'application/pdf' });

        this.pdfPreviewURL = this.sanitizer.bypassSecurityTrustResourceUrl(window.URL.createObjectURL(blob));

      },
      error: err => {
        console.error('Observable emitted an error: ' + err);
        this.isLoading = false;

      },
      complete: () => {
        console.log('Observable emitted the complete notification');
        this.isLoading = false;

      }
    });
  }

  clear(){
    this.searchModel.categoryId = undefined;
    this.searchModel.orderBy = undefined;

    this.pdfPreviewURL = undefined;
  }

  loadCategories(){
    this.lookupSerive.getCategory().subscribe((next) => {
      this.categorySelectModel = next.result;
    })
  }

}
