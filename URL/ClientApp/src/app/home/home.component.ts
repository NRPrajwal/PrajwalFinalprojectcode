import { Component } from '@angular/core';
import { HttpClient } from '@angular/common/http';



@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.css']
})
export class HomeComponent {
  originalUrl: any;
  shortUrl: any ;

  constructor(private http: HttpClient) { }

  shortenUrl()
  {
    debugger
    console.log("enter here" + this.originalUrl);
    let data = { OriginalUrl: this.originalUrl };

    const urlobject = new URL(this.originalUrl);

    this.http.post<string>('https://localhost:7180/api/Url/ShortenUrl', data).subscribe(result => {
      const presponse = JSON.stringify(result)
      this.shortUrl = presponse
      //const presponse = JSON.parse(result);
      //this.shortUrl = presponse.shortUrl
      console.log("hhhhhelppppp" + presponse);
    });
    //this.http.post<string>('https://localhost:7180/api/Url/ShortenUrl', data).subscribe
    //  (result => {
    //    console.log("hhhhhelppppp" + JSON.parse(result));
    //    this.shortUrl = result;
    //  });

   // console.log("Output" + this.shortUrl["shortUrl"]);

  }
  
}


