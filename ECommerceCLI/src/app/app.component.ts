import { Component } from '@angular/core';
import { ToastrService } from 'ngx-toastr';
import { CustomToastrService, ToastrMessageType, ToastrPosition } from './services/ui/custom-toastr.service';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrl: './app.component.scss'
})
export class AppComponent {
  title = 'ECommerceCLI';
  constructor(private toastrService: CustomToastrService) {
    toastrService.message("Hello","World",{
      messageType:ToastrMessageType.Success,
      position: ToastrPosition.BottomLeft
    });
   }

  ngOnInit(): void {
   
 
    //https://ngx-toastr.vercel.app/
}
}

$.get("https://localhost:7191/api/Product", data=>{
  console.log(data);
})

