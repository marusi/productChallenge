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
  private productNames: any[];
  private options: any[];
  private optionValues: any[];
  private skuValues: any[];

  private category: any = {
    id: 0,
    name: ''
  }

  private productName: any = {
    id: 0,
    productName: '',
    productCategoryId: 0,
  }

  private option: any = {
    id: 0,
    optionName: '',
    productId: 0,
  }

  private optionValue: any = {
    id: 0,
    optionValueName: '',
    optionId: 0,
  }

  private skuValue: any = {
    id: 0,
    skuValueName: '',
    optionValueId: 0,
  }

  private productSku: any = {
    id: 0,
    price: 0,
    skuValueId: 0,
  }


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

    this.productService.getProductNames().subscribe(productNames =>
      this.productNames = productNames)

    this.productService.getOptions().subscribe(options =>
      this.options = options)

    this.productService.getOptionValues().subscribe(optionValues =>
      this.optionValues = optionValues)

    this.productService.getSkuValues().subscribe(skuValues =>
      this.skuValues = skuValues)

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

  submitOption() {
    this.productService.createOptions(this.option)
      .subscribe(x => {
        console.log('Option Name  successfuly created')
      });
  }

  submitOptionValue() {
    this.productService.createOptionValues(this.optionValue)
      .subscribe(x => {
        console.log('Option Value  successfuly created')
      });
  }

  submitSkuValue() {
    this.productService.createSkuValues(this.skuValue)
      .subscribe(x => {
        console.log('Sku Value  successfuly created')
      });
  }

  submitProductSku() {
    this.productService.createSkuAttributes(this.productSku)
      .subscribe(x => {
        console.log('Sku Value  successfuly created')
      });
  }

}
