import { Component, OnInit } from '@angular/core';
import { AlertifyService, MessageType, Position, AlertifyOptions } from '../../services/admin/alertify.service';

@Component({
  selector: 'app-layout',
  templateUrl: './layout.component.html',
  styleUrl: './layout.component.scss'
})
export class LayoutComponent implements OnInit {

  constructor(private alertify:AlertifyService){}
ngOnInit():void{
  const options: AlertifyOptions = {
    messageType: MessageType.Message,
    position: Position.BottomCenter,
    delay: 2
  };
  this.alertify.message("Hello", options);

}
}
