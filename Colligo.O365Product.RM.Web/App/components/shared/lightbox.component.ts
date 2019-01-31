import { Component, OnInit, Input } from '@angular/core';

@Component({
    selector: 'lightbox',
    templateUrl: './lightbox.component.html',
    styles: [`
    .button
{
                width: 150px;
                padding: 10px;
                background-color: #FF8C00;
                box-shadow: -8px 8px 10px 3px rgba(0,0,0,0.2);
    font-weight:bold;
                text-decoration:none;
}
.cover{
    position:fixed;
    top:0;
    left:0;
    background:rgba(0,0,0,0.13);
    z-index:999;
    width:100%;
    height:100%;
    display:block;
}
#loginScreen
{
    height:380px;
    width:340px;
    margin:0 auto;
    position:relative;
    z-index:10;
    display:none;
                background: url(login.png) no-repeat;
                border:5px solid #cccccc;
                border-radius:10px;
}

.cancel
{
    display:block;
    position:absolute;
    top:3px;
    right:2px;
    background:rgb(245,245,245);
    color:black;
    height:30px;
    width:35px;
    font-size:30px;
    text-decoration:none;
    text-align:center;
    font-weight:bold;
}
#loginScreen:target, #loginScreen:target + #cover{
    display:block;
    opacity:2;
}
    `]
})

export class LightBoxComponent implements OnInit {
    private _showPop: boolean;
    @Input('loading') _loading:boolean;
    @Input ('loaderMessage') _loaderMessage: string;
    constructor() { }

    ngOnInit() { }
}