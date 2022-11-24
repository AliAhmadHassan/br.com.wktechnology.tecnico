import { ResponseDTO } from './../../models/responsedto.model';
import { Category } from 'src/app/models/category.model';
import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { NgxSpinnerService } from 'ngx-spinner';
import { CategoryService } from 'src/app/services/category/category.service';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';

@Component({
  selector: 'app-category-register',
  templateUrl: './category-register.component.html',
  styleUrls: ['./category-register.component.scss']
})
export class CategoryRegisterComponent implements OnInit {

  public form: FormGroup;
  category: Category = new Category();
  isNew = false;

  //#region forms input
  idFormControl = this.fb.control(null, {validators: [Validators.required], updateOn: "blur"});
  nameFormControl = this.fb.control(null, {validators: [Validators.required], updateOn: "blur"});
  descriptionFormControl = this.fb.control(null, {validators: [], updateOn: "blur"});
  //#endregion

  constructor(public route: ActivatedRoute,
    public router: Router,
    private spinner: NgxSpinnerService,
    private categoryService: CategoryService,
    private fb: FormBuilder) { 
      this.form = this.fb.group({
        idFormControl : this.idFormControl,
        nameFormControl : this.nameFormControl,
        descriptionFormControl : this.descriptionFormControl
      });

      
    }

  ngOnInit(): void {
    const id = this.route.snapshot.paramMap.get("id");
    if(id == null){
      this.isNew = true;
    }

    if(!this.isNew){
    this.spinner.show();
      this.categoryService.getById(Number(id)).subscribe((response: ResponseDTO)=>{
        let category: Category = response.data;
        this.category = category;
        this.idFormControl.setValue(category.id);
        this.nameFormControl.setValue(category.name);
        this.descriptionFormControl.setValue(category.description);
        this.spinner.hide();
      })
    }
  }

  btnSalvar_click(){
    this.spinner.show();
    this.category.description = this.descriptionFormControl.value;
    this.category.name = this.nameFormControl.value;
    
    if(this.isNew){
      this.categoryService.post(this.category).subscribe((response: ResponseDTO)=>{
        this.spinner.hide();
        this.category = response.data;
        this.router.navigate(['']);
      })
    } else {
      this.categoryService.put(this.category).subscribe((response: ResponseDTO)=> {
        this.spinner.hide();
        this.category = response.data;
        this.router.navigate(['']);
      })
    }
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
