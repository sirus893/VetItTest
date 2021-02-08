import { Component, OnInit } from '@angular/core';
import { ProductModel } from '../../models/product.model';
import { ProductService } from '../../Services/product.service';

@Component({
  selector: 'app-product',
  templateUrl: './product.component.html',
  styles: [
  ]
})
export class ProductComponent implements OnInit {

  public productList: ProductModel;

  ModalTitle: string;
  prod: any;
  ActivateEditDescription: boolean = false;

  constructor(public service:ProductService) { }

  // Load up all products at the start.
  // I've chosen to use services to share productList amongst the calls.I found this the easiest way for me
  // to allow the page to change data based on the api call.
  ngOnInit(): void {
    this.getAllProducts();
    this.service.currentProductList.subscribe(productList => this.productList = productList);
  }


  // Call api and update shared product list
  getAllProducts() {
    this.service.getAllProducts().subscribe(data => {
      this.service.changeProductList(data);
      
    });
  }


  // When we close edit, I want to refresh the products.
  // for now just get all products. But we need something that just refreshed the one row really.
  closeClick() {
    this.ActivateEditDescription = false;
    this.getAllProducts();
  }

  // We're gonna open up the bootstrap modal here where we can edit the description.
  editClick(product) {
    this.prod = product;
    this.ModalTitle = "Edit Product Description";
    this.ActivateEditDescription = true;
  }
}
