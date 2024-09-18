import { CommonModule, JsonPipe, NgFor } from '@angular/common';
import { HttpClient } from '@angular/common/http';
import { Component, inject, OnInit } from '@angular/core';
import { ResultModel } from '../../models/responsemodel';
import { UserModel } from '../../models/usermodel';

@Component({
  selector: 'app-user',
  standalone: true,
  imports: [CommonModule],
  templateUrl: './user.component.html',
  styleUrl: './user.component.css'
})
export class UserComponent implements OnInit {

  http = inject(HttpClient);
  userList: UserModel[] = [];

  ngOnInit(): void {
    this.getAllUser();
  }

  getAllUser() {
    this.http.get<ResultModel<UserModel[]>>("https://localhost:7220/api/Users")
      .subscribe((res: ResultModel<UserModel[]>) => {
        if (res.data) {
          this.userList = res.data;
          console.log(this.userList);
        }
        console.log(res, 'res');
      });
  }
}



