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
  channelname= "";
  ngOnInit() {
  }

  getApiData() {
    const url = "http://localhost:54349/api/values/?query="+this.channelname;
    console.log('url is : ' + url);
    this.http.get(url).subscribe(data => {this.retGetData = data});

    }
    
  }




