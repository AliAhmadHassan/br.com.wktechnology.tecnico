import { ResponseDTO } from './../../models/responsedto.model';
import { CategoryService } from './../../services/category/category.service';
import { Category } from './../../models/category.model';
import { ActivatedRoute, Router } from '@angular/router';
import { NgxSpinnerService } from 'ngx-spinner';
import { Product } from './../../models/product.model';
import { Component, OnInit } from '@angular/core';
import { ProductService } from 'src/app/services/product/product.service';

import swal from 'sweetalert2';

@Component({
  selector: 'app-product-list',
  templateUrl: './product-list.component.html',
  styleUrls: ['./product-list.component.scss']
})
export class ProductListComponent implements OnInit {

  products: Product[] = [];
  category: Category;
  constructor(public route: ActivatedRoute,
    public router: Router,
    private spinner: NgxSpinnerService,
    private productService: ProductService,
    private categoruService: CategoryService) { }

  ngOnInit(): void {
    const idcategory = Number(this.route.snapshot.paramMap.get("idcategory"));

    this.categoruService.getById(idcategory).subscribe((response: ResponseDTO) => {
      this.category = response.data;
    })

    this.productService.getByIdCategory(idcategory).subscribe((response: ResponseDTO) => {
      this.products = response.data;
    })
  }


  btnDelete_Click(product: Product) {
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
        this.productService.delete(product.id).subscribe(() => {
          this.spinner.hide();
          swal.fire({
            title: 'Deletado com sucesso.',
            buttonsStyling: false,
            customClass: {
              confirmButton: 'btn btn-success',
            },
            icon: 'success',
          });

          this.productService.get().subscribe((response: ResponseDTO) => {
            this.products = response.data;
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

