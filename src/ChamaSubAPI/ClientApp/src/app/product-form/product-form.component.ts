import { Component, OnInit, ViewChild } from '@angular/core';
import { ProductService } from './../services/product.service';


import { ActivatedRoute, Router } from '@angular/router';



// import { SaveProduct } from '../models/product';


@Component({
  selector: 'app-product-form',
  templateUrl: './product-form.component.html',
  styleUrls: ['./product-form.component.css']
})

export class ProductFormComponent implements OnInit {

  private categoryItems: any[];

  private category: any = {
    id: 0,
    name: ''
  }

  private productName: any = {
    id: 0,
    productName: '',
    productCategoryId: 0,
  }
 /* private product: any = {
    id: 0,
    categoryItemId: 0,
    subCategoryItemId: 0,
    suppliers: [],
    info: {},
    sell: {}
  }; */

  constructor(
    private productService: ProductService,
  
    private route: ActivatedRoute,
    private router: Router,
  
  ) {
    route.params.subscribe(p => {
    //  this.product.id = +p['id'] || 0;
    });

  }
    
  ngOnInit(): void {

    this.productService.getCategoryItems().subscribe(categoryItems =>
      this.categoryItems = categoryItems);

   



  //  this.productService.getSuppliers().subscribe(suppliers =>
   //   this.suppliers = suppliers);

  




  }






/*  onCategoryItemChange() {

    const selectedCategoryItem = this.categoryItems.find(c => c.id == this.product.categoryItemId);

    this.subCategoryItems = selectedCategoryItem ? selectedCategoryItem.subCategoryItems : [];

  }

  onSupplierToggle(supplierId, $event) {
    if ($event.target.checked)
      this.product.suppliers.push(supplierId);
    else {
      const index = this.product.suppliers.indexOf(supplierId);
      this.product.suppliers.splice(index, 1);
    }

  }

  submitProduct() {
    this.productService.create(this.product)
      .subscribe(x => {
  
        this.snackBarService.showSnackBar('Product created successfuly');
      });
  } */
  submitCategory() {
    this.productService.createCategory(this.category)
      .subscribe(x => {
        // toast notification to be provided here
       // console.log('category  successfuly created')
      });
  }

  submitProductName() {
    this.productService.createProductName(this.productName)
      .subscribe(x => {
        console.log('name  successfuly created')
      });
  }



}
