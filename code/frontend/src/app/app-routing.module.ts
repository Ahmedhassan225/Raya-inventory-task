import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { AppComponent } from './page/app.component';
import { ProductReportPage } from './features/report/pages/product-report/product-report.page';
import { NotFoundComponent } from './page/not-found/not-found.component';

const routes: Routes = [
  {
    path: '',
    loadChildren: () => import('src/app/features/product').then(m => m.ProductModule),
  
  },
  {
    path: 'report',
    loadChildren: () => import('src/app/features/report').then(m => m.ReportModule),
  },
  {    
    path: '**',
    component: NotFoundComponent
  }
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
