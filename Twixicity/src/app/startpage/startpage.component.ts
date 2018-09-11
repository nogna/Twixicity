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
    console.log("HEjeje5?query=admiralbulldog");
    const url = "https://localhost:44314/api/values/";
    this.http.get(url).subscribe(data => {this.retGetData = data});
    }
    
  }




