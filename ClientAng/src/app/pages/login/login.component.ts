import { HttpClient } from '@angular/common/http';
import { Component, inject } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { Router } from '@angular/router';
import { jwtDecode } from 'jwt-decode';


interface JwtToken {

  "http://schemas.microsoft.com/ws/2008/06/identity/claims/role": string;

}
@Component({
  selector: 'app-login',
  standalone: true,
  imports: [FormsModule],
  templateUrl: './login.component.html',
  styleUrl: './login.component.css'
})
export class LoginComponent {
  loginObj: any =
    {
      "Mail": "",
      "Password": ""
    };



  http = inject(HttpClient);
  router = inject(Router);
  onLogin() {
    this.http.post("https://localhost:7220/api/Users/login", this.loginObj).subscribe((res: any) => {
      if (res.statusCode === 200) {
        alert(res.statusMessage)
        localStorage.setItem('token', res.data.token);
        const token = res.data.token;
        const jwtToken: JwtToken = jwtDecode<JwtToken>(token);
        const roleId = jwtToken["http://schemas.microsoft.com/ws/2008/06/identity/claims/role"];
        if (roleId == "1") {
          this.router.navigateByUrl('admin')
        }
        if (roleId == "2") {
          this.router.navigateByUrl('satin-alma')
        }
       
      }
      else {
        alert(res.ErrorMessages)
      }
    });
  }
}


