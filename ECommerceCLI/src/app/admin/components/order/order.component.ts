import { Component, OnInit } from '@angular/core';
import { NgxSpinnerService } from 'ngx-spinner';

@Component({
  selector: 'app-order',
  templateUrl: './order.component.html',
  styleUrl: './order.component.scss'
})
export class OrderComponent implements OnInit {
constructor (private spinner: NgxSpinnerService){}
ngOnInit():void{
  this.spinner.show("s2");

  setTimeout(() => {
    this.spinner.hide("s2");
  }, 5000);
}
}
