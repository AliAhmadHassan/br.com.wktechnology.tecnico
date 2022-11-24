import { ResponseDTO } from './../../models/responsedto.model';
import { Category } from './../../models/category.model';
import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { NgxSpinnerService } from 'ngx-spinner';
import { Product } from 'src/app/models/product.model';
import { CategoryService } from 'src/app/services/category/category.service';
import { ProductService } from 'src/app/services/product/product.service';

@Component({
  selector: 'app-product-register',
  templateUrl: './product-register.component.html',
  styleUrls: ['./product-register.component.scss']
})
export class ProductRegisterComponent implements OnInit {

  public form: FormGroup;
  product: Product = new Product();
  categories: Category[] = [];
  category: Category;
  isNew = false;

  //#region forms input
  idFormControl = this.fb.control(null, { validators: [Validators.required], updateOn: "blur" });
  nameFormControl = this.fb.control(null, { validators: [Validators.required], updateOn: "blur" });
  descriptionFormControl = this.fb.control(null, { validators: [], updateOn: "blur" });
  unitPriceFormControl = this.fb.control(null, { validators: [Validators.required], updateOn: "blur" });
  amountFormControl = this.fb.control(null, { validators: [Validators.required], updateOn: "blur" });
  idCategoryFormControl = this.fb.control(null, { validators: [Validators.required], updateOn: "blur" });
  //#endregion

  constructor(public route: ActivatedRoute,
    public router: Router,
    private spinner: NgxSpinnerService,
    private productService: ProductService,
    private categoryService: CategoryService,
    private fb: FormBuilder) {
    this.form = this.fb.group({
      idFormControl: this.idFormControl,
      nameFormControl: this.nameFormControl,
      descriptionFormControl: this.descriptionFormControl,
      unitPriceFormControl: this.unitPriceFormControl,
      amountFormControl: this.amountFormControl,
      idCategoryFormControl: this.idCategoryFormControl
    });


  }

  ngOnInit(): void {
    const id = this.route.snapshot.paramMap.get("id");
    const idCategory = Number(this.route.snapshot.paramMap.get("idcategory"));
    if (id == null) {
      this.isNew = true;
    }

    if (!this.isNew) {
      this.spinner.show();
      this.productService.getById(Number(id)).subscribe((response: ResponseDTO) => {
        let product: Product = response.data;
        this.product = product;
        this.idFormControl.setValue(product.id);
        this.nameFormControl.setValue(product.name);
        this.descriptionFormControl.setValue(product.description);
        this.unitPriceFormControl.setValue(product.unitPrice);
        this.amountFormControl.setValue(product.amount);
        this.idCategoryFormControl.setValue(product.category.id);
        this.spinner.hide();
      })
    }
    
    this.categoryService.get().subscribe((response: ResponseDTO)=>{
      this.categories = response.data;
    })

    this.categoryService.getById(idCategory).subscribe((response: ResponseDTO)=>{
      this.category = response.data;
    })
  }

  btnSalvar_click() {
    //this.spinner.show();
    this.product.description = this.descriptionFormControl.value;
    this.product.name = this.nameFormControl.value;
    this.product.unitPrice = Number(this.unitPriceFormControl.value);
    this.product.amount = Number(this.amountFormControl.value);
    if (this.product.category == null || this.product.category == undefined)
      this.product.category = new Category();

    this.product.category.id = Number(this.idCategoryFormControl.value);
    this.product.category.name = 'bind';

    if (this.isNew) {
      console.log(JSON.stringify(this.product));
      this.productService.post(this.product).subscribe((respose: ResponseDTO) => {
        this.spinner.hide();
        this.product = respose.data;
        this.router.navigate(['product-list', this.product.category.id]);
      })
    } else {
      this.productService.put(this.product).subscribe((respose: ResponseDTO) => {
        this.spinner.hide();
        this.product = respose.data;
        this.router.navigate(['product-list', this.product.category.id]);
      })
    }
  }

  changeCategory(event: any){
    this.idCategoryFormControl.setValue(event.value);
  }
  isFieldValid(form: FormGroup, field: string) {
    return !form.get(field).valid && form.get(field).touched;
  }
  displayFieldCss(form: FormGroup, field: string) {
    return {
      'has-error': this.isFieldValid(form, field),
      'has-feedback': this.isFieldValid(form, field)
    };
  }

}
