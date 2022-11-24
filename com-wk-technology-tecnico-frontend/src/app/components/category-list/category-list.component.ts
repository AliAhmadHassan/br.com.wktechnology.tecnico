import { Category } from './../../models/category.model';
import { CategoryService } from './../../services/category/category.service';
import { NgxSpinnerService } from 'ngx-spinner';
import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';

import swal from 'sweetalert2';
import { ResponseDTO } from 'src/app/models/responsedto.model';

@Component({
  selector: 'app-category-list',
  templateUrl: './category-list.component.html',
  styleUrls: ['./category-list.component.scss']
})
export class CategoryListComponent implements OnInit {

  categories: Category[] = [];

  constructor(public route: ActivatedRoute,
    public router: Router,
    private spinner: NgxSpinnerService,
    private categoryService: CategoryService) { }

  ngOnInit(): void {
    this.spinner.show();
    this.categoryService.get().subscribe((response: ResponseDTO)=> {
      this.categories = response.data;
      this.spinner.hide();
    })
  }

  btnEditar_Click(category:Category){
    this.router.navigate(['category-register', category.id])
  }

  btnProducts_Click(category:Category){
    this.router.navigate(['product-list', category.id]);
  }

  btnDelete_Click(category:Category){
    swal.fire({
      title: "Tem certeza? ",
      text: `Que deseja deletar?`,
      icon: 'warning',
      showCancelButton: true,
      confirmButtonText: "Sim, pode apagar",
      cancelButtonText: "Não",
      customClass: {
        confirmButton: "btn btn-success",
        cancelButton: "btn btn-danger mx-2",
      },
      buttonsStyling: false
    }).then((result) => {
      if (result.value) {
        this.spinner.show();
        this.categoryService.delete(category.id).subscribe(() => {
            this.spinner.hide();
            swal.fire({
              title: 'Deletado com sucesso.',
              buttonsStyling: false,
              customClass: {
                confirmButton: 'btn btn-success',
              },
              icon: 'success',
            });

            this.categoryService.get().subscribe((response: ResponseDTO)=> {
              this.categories = response.data;
              this.spinner.hide();
            })
        }, (error: any) => {
          this.spinner.hide();
          console.log(JSON.stringify(error));
          swal.fire({
            title: 'Falha ao deletar.',
            buttonsStyling: false,
            customClass: {
              confirmButton: 'btn btn-danger',
            },
            icon: 'warning',
          });
        });
      }
    });
  }

}
