import { ProductRegisterComponent } from './components/product-register/product-register.component';
import { ProductListComponent } from './components/product-list/product-list.component';
import { CategoryRegisterComponent } from './components/category-register/category-register.component';
import { CategoryListComponent } from './components/category-list/category-list.component';
import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';

const routes: Routes = [{
  path: "",
  component: CategoryListComponent,
  pathMatch: "full"
},{
  path: "category-register",
  component: CategoryRegisterComponent,
  pathMatch: "full"
},{
  path: "category-register/:id",
  component: CategoryRegisterComponent,
  pathMatch: "full"
},{
  path: "product-list/:idcategory",
  component: ProductListComponent,
  pathMatch: "full"
},{
  path: "product-register/:idcategory",
  component: ProductRegisterComponent,
  pathMatch: "full"
},{
  path: "product-register/:idcategory/:id",
  component: ProductRegisterComponent,
  pathMatch: "full"
}];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
