import { Component, OnInit } from '@angular/core';
import { HttpClient, HttpHeaders} from '@angular/common/http';

@Component({
  selector: 'app-startpage',
  templateUrl: './startpage.component.html',
  styleUrls: ['./startpage.component.scss']
})
export class StartpageComponent implements OnInit {

  constructor(private http: HttpClient) { }
  public retGetData;

  ngOnInit() {
  }

  getApiData() {
    console.log("HEjeje5");
    const url = "http://localhost:54349/api/values/5";
    this.http.get(url).subscribe(data => {this.retGetData = data});
    }
    
  }




