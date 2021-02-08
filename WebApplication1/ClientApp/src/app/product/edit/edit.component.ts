import { Component, Input, OnInit } from '@angular/core';
import { ProductModel } from '../../../models/product.model';
import { ProductService } from '../../../Services/product.service';

@Component({
  selector: 'app-edit',
  templateUrl: './edit.component.html',
  styles: []
})
export class EditComponent implements OnInit {

  constructor(private service: ProductService) { }

  @Input() prod: ProductModel;
  ProductId: number;
  ProductDescription: string;


  ngOnInit() {
    this.ProductId = this.prod.productId;
    this.ProductDescription = this.prod.productDescription;
  }


  updateProduct() {
    this.service.updateDescription(this.ProductId, this.ProductDescription).subscribe(res => {
      if (res !== null && res['productDescription'] === this.ProductDescription) {
        alert("Description has been updated")
      }
      else {
        alert("Description did not update");
      }


    })
  }
}
