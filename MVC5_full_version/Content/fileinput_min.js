From: "Guardado por Internet Explorer 11"
Subject: 
Date: Thu, 30 Jun 2016 17:55:24 -0500
MIME-Version: 1.0
Content-Type: text/html;
	charset="utf-8"
Content-Transfer-Encoding: quoted-printable
Content-Location: http://plugins.krajee.com/assets/82d92011/js/fileinput.min.js
X-MimeOLE: Produced By Microsoft MimeOLE V10.0.10011.16384

=EF=BB=BF<!DOCTYPE HTML>
<!DOCTYPE html PUBLIC "" ""><HTML><HEAD><META content=3D"IE=3D11.0000"=20
http-equiv=3D"X-UA-Compatible">

<META http-equiv=3D"Content-Type" content=3D"text/html; =
charset=3Dutf-8">
<META name=3D"GENERATOR" content=3D"MSHTML 11.00.10586.420"></HEAD>
<BODY>
<PRE>/*!=0A=
 * bootstrap-fileinput v4.3.2=0A=
 * http://plugins.krajee.com/file-input=0A=
 *=0A=
 * Author: Kartik Visweswaran=0A=
 * Copyright: 2014 - 2016, Kartik Visweswaran, Krajee.com=0A=
 *=0A=
 * Licensed under the BSD 3-Clause=0A=
 * https://github.com/kartik-v/bootstrap-fileinput/blob/master/LICENSE.md=0A=
 */=0A=
!function(e){"use strict";"function"=3D=3Dtypeof =
define&amp;&amp;define.amd?define(["jquery"],e):"object"=3D=3Dtypeof =
module&amp;&amp;module.exports?module.exports=3De(require("jquery")):e(wi=
ndow.jQuery)}(function(e){"use =
strict";e.fn.fileinputLocales=3D{},e.fn.fileinputThemes=3D{};var =
t,i,a,n,r,l,o,s,d,c,p,u,f,m,g,v,h,w,b,_,C,x,y,T,F,k,E,$,S,I,P,D,z,A,U,j,L=
,Z,B,O,R,M,N,H,q,W,V,K,G,X,Y,J,Q,ee,te,ie,ae,ne,re,le,oe,se,de,ce,pe,ue,f=
e,me,ge;t=3D".fileinput",i=3D"kvFileinputModal",a=3D'style=3D"width:{widt=
h};height:{height};"',n=3D'&lt;param name=3D"controller" value=3D"true" =
/&gt;\n&lt;param name=3D"allowFullScreen" value=3D"true" =
/&gt;\n&lt;param name=3D"allowScriptAccess" value=3D"always" =
/&gt;\n&lt;param name=3D"autoPlay" value=3D"false" /&gt;\n&lt;param =
name=3D"autoStart" value=3D"false" /&gt;\n&lt;param name=3D"quality" =
value=3D"high" /&gt;\n',r=3D'&lt;div =
class=3D"file-preview-other"&gt;\n&lt;span =
class=3D"{previewFileIconClass}"&gt;{previewFileIcon}&lt;/span&gt;\n&lt;/=
div&gt;',l=3Dwindow.URL||window.webkitURL,o=3Dfunction(e,t,i){return =
void =
0!=3D=3De&amp;&amp;(i?e=3D=3D=3Dt:e.match(t))},s=3Dfunction(e){if("Micros=
oft Internet =
Explorer"!=3D=3Dnavigator.appName)return!1;if(10=3D=3D=3De)return new =
RegExp("msie\\s"+e,"i").test(navigator.userAgent);var =
t,i=3Ddocument.createElement("div");return i.innerHTML=3D"&lt;!--[if IE =
"+e+"]&gt; &lt;i&gt;&lt;/i&gt; =
&lt;![endif]--&gt;",t=3Di.getElementsByTagName("i").length,document.body.=
appendChild(i),i.parentNode.removeChild(i),t},d=3Dfunction(){return new =
RegExp("Edge/[0-9]+","i").test(navigator.userAgent)},c=3Dfunction(e,i,a,n=
){var r=3Dn?i:i.split(" ").join(t+" =
")+t;e.off(r).on(r,a)},p=3D{data:{},init:function(e){var =
t=3De.initialPreview,i=3De.id;t.length&gt;0&amp;&amp;!re(t)&amp;&amp;(t=3D=
t.split(e.initialPreviewDelimiter)),p.data[i]=3D{content:t,config:e.initi=
alPreviewConfig,tags:e.initialPreviewThumbTags,delimiter:e.initialPreview=
Delimiter,previewFileType:e.initialPreviewFileType,previewAsData:e.initia=
lPreviewAsData,template:e.previewGenericTemplate,showZoom:e.fileActionSet=
tings.showZoom,showDrag:e.fileActionSettings.showDrag,getSize:function(t)=
{return e._getSize(t)},parseTemplate:function(t,i,a,n,r,l,o){var s=3D" =
file-preview-initial";return =
e._generatePreviewTemplate(t,i,a,n,r,!1,null,s,l,o)},msg:function(t){retu=
rn =
e._getMsgSelected(t)},initId:e.previewInitId,footer:e._getLayoutTemplate(=
"footer").replace(/\{progress}/g,e._renderThumbProgress()),isDelete:e.ini=
tialPreviewShowDelete,caption:e.initialCaption,actions:function(t,i,a,n,r=
,l,o){return =
e._renderFileActions(t,i,a,n,r,l,o,!0)}}},fetch:function(e){return =
p.data[e].content.filter(function(e){return =
null!=3D=3De})},count:function(e,t){return =
p.data[e]&amp;&amp;p.data[e].content?t?p.data[e].content.length:p.fetch(e=
).length:0},get:function(t,i,a){var =
n,r,l,o,s,d,c=3D"init_"+i,u=3Dp.data[t],f=3Du.config[i],m=3Du.content[i],=
g=3Du.initId+"-"+c,v=3D" =
file-preview-initial",h=3Dle("previewAsData",f,u.previewAsData);return =
a=3Dvoid 0=3D=3D=3Da?!0:a,m?(f&amp;&amp;f.frameClass&amp;&amp;(v+=3D" =
"+f.frameClass),h?(l=3Du.previewAsData?le("type",f,u.previewFileType||"ge=
neric"):"generic",o=3Dle("caption",f),s=3Dp.footer(t,i,a,f&amp;&amp;f.siz=
e||null),d=3Dle("filetype",f,l),n=3Du.parseTemplate(l,m,o,d,g,s,c,null)):=
n=3Du.template.replace(/\{previewId}/g,g).replace(/\{frameClass}/g,v).rep=
lace(/\{fileindex}/g,c).replace(/\{content}/g,u.content[i]).replace(/\{te=
mplate}/g,le("type",f,u.previewFileType)).replace(/\{footer}/g,p.footer(t=
,i,a,f&amp;&amp;f.size||null)),u.tags.length&amp;&amp;u.tags[i]&amp;&amp;=
(n=3Dde(n,u.tags[i])),ne(f)||ne(f.frameAttr)||(r=3De(document.createEleme=
nt("div")).html(n),r.find(".file-preview-initial").attr(f.frameAttr),n=3D=
r.html(),r.remove()),n):""},add:function(t,i,a,n,r){var =
l,o=3De.extend(!0,{},p.data[t]);return =
re(i)||(i=3Di.split(o.delimiter)),r?(l=3Do.content.push(i)-1,o.config[l]=3D=
a,o.tags[l]=3Dn):(l=3Di.length-1,o.content=3Di,o.config=3Da,o.tags=3Dn),p=
.data[t]=3Do,l},set:function(t,i,a,n,r){var =
l,o,s=3De.extend(!0,{},p.data[t]);if(i&amp;&amp;i.length&amp;&amp;(re(i)|=
|(i=3Di.split(s.delimiter)),o=3Di.filter(function(e){return =
null!=3D=3De}),o.length)){if(void =
0=3D=3D=3Ds.content&amp;&amp;(s.content=3D[]),void =
0=3D=3D=3Ds.config&amp;&amp;(s.config=3D[]),void =
0=3D=3D=3Ds.tags&amp;&amp;(s.tags=3D[]),r){for(l=3D0;l&lt;i.length;l++)i[=
l]&amp;&amp;s.content.push(i[l]);for(l=3D0;l&lt;a.length;l++)a[l]&amp;&am=
p;s.config.push(a[l]);for(l=3D0;l&lt;n.length;l++)n[l]&amp;&amp;s.tags.pu=
sh(n[l])}else =
s.content=3Di,s.config=3Da,s.tags=3Dn;p.data[t]=3Ds}},unset:function(e,t)=
{var i=3Dp.count(e);if(i){if(1=3D=3D=3Di)return =
p.data[e].content=3D[],p.data[e].config=3D[],void(p.data[e].tags=3D[]);p.=
data[e].content[t]=3Dnull,p.data[e].config[t]=3Dnull,p.data[e].tags[t]=3D=
null}},out:function(e){var =
t,i=3D"",a=3Dp.data[e],n=3Dp.count(e,!0);if(0=3D=3D=3Dn)return{content:""=
,caption:""};for(var r=3D0;n&gt;r;r++)i+=3Dp.get(e,r);return =
t=3Da.msg(p.count(e)),{content:'&lt;div =
class=3D"file-initial-thumbs"&gt;'+i+"&lt;/div&gt;",caption:t}},footer:fu=
nction(e,t,i,a){var n=3Dp.data[e];if(i=3Dvoid =
0=3D=3D=3Di?!0:i,0=3D=3D=3Dn.config.length||ne(n.config[t]))return"";var =
r=3Dn.config[t],l=3Dle("caption",r),o=3Dle("width",r,"auto"),s=3Dle("url"=
,r,!1),d=3Dle("key",r,null),c=3Dle("showDelete",r,!0),u=3Dle("showZoom",r=
,n.showZoom),f=3Dle("showDrag",r,n.showDrag),m=3Ds=3D=3D=3D!1&amp;&amp;i,=
g=3Dn.isDelete?n.actions(!1,c,u,f,m,s,d):"",v=3Dn.footer.replace(/\{actio=
ns}/g,g);return =
v.replace(/\{caption}/g,l).replace(/\{size}/g,n.getSize(a)).replace(/\{wi=
dth}/g,o).replace(/\{indicator}/g,"").replace(/\{indicatorTitle}/g,"")}},=
u=3Dfunction(e,t){return t=3Dt||0,"number"=3D=3Dtypeof =
e?e:("string"=3D=3Dtypeof =
e&amp;&amp;(e=3DparseFloat(e)),isNaN(e)?t:e)},f=3Dfunction(){return!(!win=
dow.File||!window.FileReader)},m=3Dfunction(){var =
e=3Ddocument.createElement("div");return!s(9)&amp;&amp;!d()&amp;&amp;(voi=
d 0!=3D=3De.draggable||void 0!=3D=3De.ondragstart&amp;&amp;void =
0!=3D=3De.ondrop)},g=3Dfunction(){return =
f()&amp;&amp;window.FormData},v=3Dfunction(e,t){e.removeClass(t).addClass=
(t)},X=3D{showRemove:!0,showUpload:!0,showZoom:!0,showDrag:!0,removeIcon:=
'&lt;i class=3D"glyphicon glyphicon-trash =
text-danger"&gt;&lt;/i&gt;',removeClass:"btn btn-xs =
btn-default",removeTitle:"Remove file",uploadIcon:'&lt;i =
class=3D"glyphicon glyphicon-upload =
text-info"&gt;&lt;/i&gt;',uploadClass:"btn btn-xs =
btn-default",uploadTitle:"Upload file",zoomIcon:'&lt;i =
class=3D"glyphicon glyphicon-zoom-in"&gt;&lt;/i&gt;',zoomClass:"btn =
btn-xs btn-default",zoomTitle:"View Details",dragIcon:'&lt;i =
class=3D"glyphicon =
glyphicon-menu-hamburger"&gt;&lt;/i&gt;',dragClass:"text-info",dragTitle:=
"Move / Rearrange",dragSettings:{},indicatorNew:'&lt;i =
class=3D"glyphicon glyphicon-hand-down =
text-warning"&gt;&lt;/i&gt;',indicatorSuccess:'&lt;i class=3D"glyphicon =
glyphicon-ok-sign text-success"&gt;&lt;/i&gt;',indicatorError:'&lt;i =
class=3D"glyphicon glyphicon-exclamation-sign =
text-danger"&gt;&lt;/i&gt;',indicatorLoading:'&lt;i class=3D"glyphicon =
glyphicon-hand-up text-muted"&gt;&lt;/i&gt;',indicatorNewTitle:"Not =
uploaded =
yet",indicatorSuccessTitle:"Uploaded",indicatorErrorTitle:"Upload =
Error",indicatorLoadingTitle:"Uploading ..."},h=3D'{preview}\n&lt;div =
class=3D"kv-upload-progress hide"&gt;&lt;/div&gt;\n&lt;div =
class=3D"input-group {class}"&gt;\n   {caption}\n   &lt;div =
class=3D"input-group-btn"&gt;\n       {remove}\n       {cancel}\n       =
{upload}\n       {browse}\n   =
&lt;/div&gt;\n&lt;/div&gt;',w=3D'{preview}\n&lt;div =
class=3D"kv-upload-progress =
hide"&gt;&lt;/div&gt;\n{remove}\n{cancel}\n{upload}\n{browse}\n',b=3D'&lt=
;div class=3D"file-preview {class}"&gt;\n    {close}    &lt;div =
class=3D"{dropClass}"&gt;\n    &lt;div =
class=3D"file-preview-thumbnails"&gt;\n    &lt;/div&gt;\n    &lt;div =
class=3D"clearfix"&gt;&lt;/div&gt;    &lt;div =
class=3D"file-preview-status text-center text-success"&gt;&lt;/div&gt;\n =
   &lt;div class=3D"kv-fileinput-error"&gt;&lt;/div&gt;\n    =
&lt;/div&gt;\n&lt;/div&gt;',C=3D'&lt;div class=3D"close =
fileinput-remove"&gt;&amp;times;&lt;/div&gt;\n',_=3D'&lt;i =
class=3D"glyphicon glyphicon-file =
kv-caption-icon"&gt;&lt;/i&gt;',x=3D'&lt;div tabindex=3D"500" =
class=3D"form-control file-caption {class}"&gt;\n   &lt;div =
class=3D"file-caption-name"&gt;&lt;/div&gt;\n&lt;/div&gt;\n',y=3D'&lt;but=
ton type=3D"{type}" tabindex=3D"500" title=3D"{title}" class=3D"{css}" =
{status}&gt;{icon} {label}&lt;/button&gt;',T=3D'&lt;a href=3D"{href}" =
tabindex=3D"500" title=3D"{title}" class=3D"{css}" {status}&gt;{icon} =
{label}&lt;/a&gt;',F=3D'&lt;div tabindex=3D"500" class=3D"{css}" =
{status}&gt;{icon} {label}&lt;/div&gt;',k=3D'&lt;div id=3D"'+i+'" =
class=3D"file-zoom-dialog modal fade" tabindex=3D"-1" =
aria-labelledby=3D"'+i+'Label"&gt;&lt;/div&gt;',E=3D'&lt;div =
class=3D"modal-dialog modal-lg" role=3D"document"&gt;\n  &lt;div =
class=3D"modal-content"&gt;\n    &lt;div class=3D"modal-header"&gt;\n    =
  &lt;div class=3D"kv-zoom-actions =
pull-right"&gt;{toggleheader}{fullscreen}{borderless}{close}&lt;/div&gt;\=
n      &lt;h3 class=3D"modal-title"&gt;{heading} &lt;small&gt;&lt;span =
class=3D"kv-zoom-title"&gt;&lt;/span&gt;&lt;/small&gt;&lt;/h3&gt;\n    =
&lt;/div&gt;\n    &lt;div class=3D"modal-body"&gt;\n      &lt;div =
class=3D"floating-buttons"&gt;&lt;/div&gt;\n      &lt;div =
class=3D"kv-zoom-body file-zoom-content"&gt;&lt;/div&gt;\n{prev} =
{next}\n    &lt;/div&gt;\n  &lt;/div&gt;\n&lt;/div&gt;\n',$=3D'&lt;div =
class=3D"progress"&gt;\n    &lt;div class=3D"{class}" =
role=3D"progressbar" aria-valuenow=3D"{percent}" aria-valuemin=3D"0" =
aria-valuemax=3D"100" style=3D"width:{percent}%;"&gt;\n        =
{percent}%\n     &lt;/div&gt;\n&lt;/div&gt;',S=3D" =
&lt;br&gt;&lt;samp&gt;({sizeText})&lt;/samp&gt;",I=3D'&lt;div =
class=3D"file-thumbnail-footer"&gt;\n    &lt;div =
class=3D"file-footer-caption" =
title=3D"{caption}"&gt;{caption}{size}&lt;/div&gt;\n    {progress} =
{actions}\n&lt;/div&gt;',P=3D'&lt;div class=3D"file-actions"&gt;\n    =
&lt;div class=3D"file-footer-buttons"&gt;\n        {upload} {delete} =
{zoom} {other}    &lt;/div&gt;\n    {drag}\n    &lt;div =
class=3D"file-upload-indicator" =
title=3D"{indicatorTitle}"&gt;{indicator}&lt;/div&gt;\n    &lt;div =
class=3D"clearfix"&gt;&lt;/div&gt;\n&lt;/div&gt;',D=3D'&lt;button =
type=3D"button" class=3D"kv-file-remove {removeClass}" =
title=3D"{removeTitle}" =
{dataUrl}{dataKey}&gt;{removeIcon}&lt;/button&gt;\n',z=3D'&lt;button =
type=3D"button" class=3D"kv-file-upload {uploadClass}" =
title=3D"{uploadTitle}"&gt;{uploadIcon}&lt;/button&gt;',A=3D'&lt;button =
type=3D"button" class=3D"kv-file-zoom {zoomClass}" =
title=3D"{zoomTitle}"&gt;{zoomIcon}&lt;/button&gt;',U=3D'&lt;span =
class=3D"file-drag-handle {dragClass}" =
title=3D"{dragTitle}"&gt;{dragIcon}&lt;/span&gt;',j=3D'&lt;div =
class=3D"file-preview-frame{frameClass}" id=3D"{previewId}" =
data-fileindex=3D"{fileindex}" =
data-template=3D"{template}"',L=3Dj+'&gt;&lt;div =
class=3D"kv-file-content"&gt;\n',Z=3Dj+' title=3D"{caption}" =
'+a+'&gt;&lt;div =
class=3D"kv-file-content"&gt;\n',B=3D"&lt;/div&gt;{footer}\n&lt;/div&gt;\=
n",O=3D"{content}\n",R=3D'&lt;div class=3D"kv-preview-data =
file-preview-html" title=3D"{caption}" =
'+a+"&gt;{data}&lt;/div&gt;\n",M=3D'&lt;img src=3D"{data}" =
class=3D"kv-preview-data file-preview-image" title=3D"{caption}" =
alt=3D"{caption}" '+a+"&gt;\n",N=3D'&lt;textarea =
class=3D"kv-preview-data file-preview-text" title=3D"{caption}" readonly =
'+a+"&gt;{data}&lt;/textarea&gt;\n",H=3D'&lt;video =
class=3D"kv-preview-data" width=3D"{width}" height=3D"{height}" =
controls&gt;\n&lt;source src=3D"{data}" =
type=3D"{type}"&gt;\n'+r+"\n&lt;/video&gt;\n",q=3D'&lt;audio =
class=3D"kv-preview-data" controls&gt;\n&lt;source src=3D"{data}" =
type=3D"{type}"&gt;\n'+r+"\n&lt;/audio&gt;\n",W=3D'&lt;object =
class=3D"kv-preview-data file-object" =
type=3D"application/x-shockwave-flash" width=3D"{width}" =
height=3D"{height}" data=3D"{data}"&gt;\n'+n+" =
"+r+"\n&lt;/object&gt;\n",V=3D'&lt;object class=3D"kv-preview-data =
file-object" data=3D"{data}" type=3D"{type}" width=3D"{width}" =
height=3D"{height}"&gt;\n&lt;param name=3D"movie" value=3D"{caption}" =
/&gt;\n'+n+" "+r+"\n&lt;/object&gt;\n",K=3D'&lt;embed =
class=3D"kv-preview-data" src=3D"{data}" width=3D"{width}" =
height=3D"{height}" type=3D"application/pdf"&gt;\n',G=3D'&lt;div =
class=3D"kv-preview-data =
file-preview-other-frame"&gt;\n'+r+"\n&lt;/div&gt;\n",Y=3D{main1:h,main2:=
w,preview:b,close:C,fileIcon:_,caption:x,modalMain:k,modal:E,progress:$,s=
ize:S,footer:I,actions:P,actionDelete:D,actionUpload:z,actionZoom:A,actio=
nDrag:U,btnDefault:y,btnLink:T,btnBrowse:F},J=3D{generic:L+O+B,html:L+R+B=
,image:L+M+B,text:L+N+B,video:Z+H+B,audio:Z+q+B,flash:Z+W+B,object:Z+V+B,=
pdf:Z+K+B,other:Z+G+B},ee=3D["image","html","text","video","audio","flash=
","pdf","object"],ie=3D{image:{width:"auto",height:"160px"},html:{width:"=
213px",height:"160px"},text:{width:"213px",height:"160px"},video:{width:"=
213px",height:"160px"},audio:{width:"213px",height:"80px"},flash:{width:"=
213px",height:"160px"},object:{width:"160px",height:"160px"},pdf:{width:"=
160px",height:"160px"},other:{width:"160px",height:"160px"}},Q=3D{image:{=
width:"100%",height:"100%"},html:{width:"100%",height:"100%","min-height"=
:"480px"},text:{width:"100%",height:"100%","min-height":"480px"},video:{w=
idth:"auto",height:"100%","max-width":"100%"},audio:{width:"100%",height:=
"30px"},flash:{width:"auto",height:"480px"},object:{width:"auto",height:"=
100%","min-height":"480px"},pdf:{width:"100%",height:"100%","min-height":=
"480px"},other:{width:"auto",height:"100%","min-height":"480px"}},ae=3D{i=
mage:function(e,t){return =
o(e,"image.*")||o(t,/\.(gif|png|jpe?g)$/i)},html:function(e,t){return =
o(e,"text/html")||o(t,/\.(htm|html)$/i)},text:function(e,t){return =
o(e,"text.*")||o(t,/\.(xml|javascript)$/i)||o(t,/\.(txt|md|csv|nfo|ini|js=
on|php|js|css)$/i)},video:function(e,t){return =
o(e,"video.*")&amp;&amp;(o(e,/(ogg|mp4|mp?g|webm|3gp)$/i)||o(t,/\.(og?|mp=
4|webm|mp?g|3gp)$/i))},audio:function(e,t){return =
o(e,"audio.*")&amp;&amp;(o(t,/(ogg|mp3|mp?g|wav)$/i)||o(t,/\.(og?|mp3|mp?=
g|wav)$/i))},flash:function(e,t){return =
o(e,"application/x-shockwave-flash",!0)||o(t,/\.(swf)$/i)},pdf:function(e=
,t){return =
o(e,"application/pdf",!0)||o(t,/\.(pdf)$/i)},object:function(){return!0},=
other:function(){return!0}},ne=3Dfunction(t,i){return void =
0=3D=3D=3Dt||null=3D=3D=3Dt||0=3D=3D=3Dt.length||i&amp;&amp;""=3D=3D=3De.=
trim(t)},re=3Dfunction(e){return Array.isArray(e)||"[object =
Array]"=3D=3D=3DObject.prototype.toString.call(e)},le=3Dfunction(e,t,i){r=
eturn i=3Di||"","object"=3D=3Dtypeof t&amp;&amp;e in =
t?t[e]:i},te=3Dfunction(t,i,a){return =
ne(t)||ne(t[i])?a:e(t[i])},oe=3Dfunction(){return Math.round((new =
Date).getTime()+100*Math.random())},se=3Dfunction(e){return =
e.replace(/&amp;/g,"&amp;amp;").replace(/&lt;/g,"&amp;lt;").replace(/&gt;=
/g,"&amp;gt;").replace(/"/g,"&amp;quot;").replace(/'/g,"&amp;apos;")},de=3D=
function(t,i){var a=3Dt;return =
i?(e.each(i,function(e,t){"function"=3D=3Dtypeof =
t&amp;&amp;(t=3Dt()),a=3Da.split(e).join(t)}),a):a},ce=3Dfunction(e){var =
t=3De.is("img")?e.attr("src"):e.find("source").attr("src");l.revokeObject=
URL(t)},pe=3Dfunction(e){var =
t=3De.lastIndexOf("/");return-1=3D=3D=3Dt&amp;&amp;(t=3De.lastIndexOf("\\=
")),e.split(e.substring(t,t+1)).pop()},ue=3Dfunction(){return =
document.fullscreenElement||document.mozFullScreenElement||document.webki=
tFullscreenElement||document.msFullscreenElement},fe=3Dfunction(e){e&amp;=
&amp;!ue()?document.documentElement.requestFullscreen?document.documentEl=
ement.requestFullscreen():document.documentElement.msRequestFullscreen?do=
cument.documentElement.msRequestFullscreen():document.documentElement.moz=
RequestFullScreen?document.documentElement.mozRequestFullScreen():documen=
t.documentElement.webkitRequestFullscreen&amp;&amp;document.documentEleme=
nt.webkitRequestFullscreen(Element.ALLOW_KEYBOARD_INPUT):document.exitFul=
lscreen?document.exitFullscreen():document.msExitFullscreen?document.msEx=
itFullscreen():document.mozCancelFullScreen?document.mozCancelFullScreen(=
):document.webkitExitFullscreen&amp;&amp;document.webkitExitFullscreen()}=
,me=3Dfunction(e,t,i){if(i&gt;=3De.length)for(var =
a=3Di-e.length;a--+1;)e.push(void 0);return =
e.splice(i,0,e.splice(t,1)[0]),e},ge=3Dfunction(t,i){var =
a=3Dthis;a.$element=3De(t),a._validate()&amp;&amp;(a.isPreviewable=3Df(),=
a.isIE9=3Ds(9),a.isIE10=3Ds(10),a.isPreviewable||a.isIE9?(a._init(i),a._l=
isten()):a.$element.removeClass("file-loading"))},ge.prototype=3D{constru=
ctor:ge,_init:function(t){var =
i,a=3Dthis,n=3Da.$element;e.each(t,function(e,t){switch(e){case"minFileCo=
unt":case"maxFileCount":case"maxFileSize":a[e]=3Du(t);break;default:a[e]=3D=
t}}),ne(a.allowedPreviewTypes)&amp;&amp;(a.allowedPreviewTypes=3Dee),a.fi=
leInputCleared=3D!1,a.fileBatchCompleted=3D!0,a.isPreviewable||(a.showPre=
view=3D!1),a.uploadFileAttr=3Dne(n.attr("name"))?"file_data":n.attr("name=
"),a.reader=3Dnull,a.formdata=3D{},a.clearStack(),a.uploadCount=3D0,a.upl=
oadStatus=3D{},a.uploadLog=3D[],a.uploadAsyncCount=3D0,a.loadedImages=3D[=
],a.totalImagesCount=3D0,a.ajaxRequests=3D[],a.isError=3D!1,a.ajaxAborted=
=3D!1,a.cancelling=3D!1,i=3Da._getLayoutTemplate("progress"),a.progressTe=
mplate=3Di.replace("{class}",a.progressClass),a.progressCompleteTemplate=3D=
i.replace("{class}",a.progressCompleteClass),a.progressErrorTemplate=3Di.=
replace("{class}",a.progressErrorClass),a.dropZoneEnabled=3Dm()&amp;&amp;=
a.dropZoneEnabled,a.isDisabled=3Da.$element.attr("disabled")||a.$element.=
attr("readonly"),a.isUploadable=3Dg()&amp;&amp;!ne(a.uploadUrl),a.isClick=
able=3Da.browseOnZoneClick&amp;&amp;a.showPreview&amp;&amp;(a.isUploadabl=
e&amp;&amp;a.dropZoneEnabled||!ne(a.defaultPreviewContent)),a.slug=3D"fun=
ction"=3D=3Dtypeof =
t.slugCallback?t.slugCallback:a._slugDefault,a.mainTemplate=3Da.showCapti=
on?a._getLayoutTemplate("main1"):a._getLayoutTemplate("main2"),a.captionT=
emplate=3Da._getLayoutTemplate("caption"),a.previewGenericTemplate=3Da._g=
etPreviewTemplate("generic"),a.resizeImage&amp;&amp;(a.maxImageWidth||a.m=
axImageHeight)&amp;&amp;(a.imageCanvas=3Ddocument.createElement("canvas")=
,a.imageCanvasContext=3Da.imageCanvas.getContext("2d")),ne(a.$element.att=
r("id"))&amp;&amp;a.$element.attr("id",oe()),void =
0=3D=3D=3Da.$container?a.$container=3Da._createContainer():a._refreshCont=
ainer(),a.$dropZone=3Da.$container.find(".file-drop-zone"),a.$progress=3D=
a.$container.find(".kv-upload-progress"),a.$btnUpload=3Da.$container.find=
(".fileinput-upload"),a.$captionContainer=3Dte(t,"elCaptionContainer",a.$=
container.find(".file-caption")),a.$caption=3Dte(t,"elCaptionText",a.$con=
tainer.find(".file-caption-name")),a.$previewContainer=3Dte(t,"elPreviewC=
ontainer",a.$container.find(".file-preview")),a.$preview=3Dte(t,"elPrevie=
wImage",a.$container.find(".file-preview-thumbnails")),a.$previewStatus=3D=
te(t,"elPreviewStatus",a.$container.find(".file-preview-status")),a.$erro=
rContainer=3Dte(t,"elErrorContainer",a.$previewContainer.find(".kv-filein=
put-error")),ne(a.msgErrorClass)||v(a.$errorContainer,a.msgErrorClass),a.=
$errorContainer.hide(),a.fileActionSettings=3De.extend(!0,X,t.fileActionS=
ettings),a.previewInitId=3D"preview-"+oe(),a.id=3Da.$element.attr("id"),p=
.init(a),a._initPreview(!0),a._initPreviewActions(),a.options=3Dt,a._setF=
ileDropZoneTitle(),a.$element.removeClass("file-loading"),a.$element.attr=
("disabled")&amp;&amp;a.disable(),a._initZoom()},_validate:function(){var=
 =
e,t=3Dthis;return"file"=3D=3D=3Dt.$element.attr("type")?!0:(e=3D'&lt;div =
class=3D"help-block alert alert-warning"&gt;&lt;h4&gt;Invalid Input =
Type&lt;/h4&gt;You must set an input &lt;code&gt;type =3D =
file&lt;/code&gt; for &lt;b&gt;bootstrap-fileinput&lt;/b&gt; plugin to =
initialize.&lt;/div&gt;',t.$element.after(e),!1)},_errorsExist:function()=
{var t,i=3Dthis;return =
i.$errorContainer.find("li").length?!0:(t=3De(document.createElement("div=
")).html(i.$errorContainer.html()),t.find("span.kv-error-close").remove()=
,t.find("ul").remove(),!!e.trim(t.text()).length)},_errorHandler:function=
(e,t){var =
i=3Dthis,a=3De.target.error;a.code=3D=3D=3Da.NOT_FOUND_ERR?i._showError(i=
.msgFileNotFound.replace("{name}",t)):a.code=3D=3D=3Da.SECURITY_ERR?i._sh=
owError(i.msgFileSecured.replace("{name}",t)):a.code=3D=3D=3Da.NOT_READAB=
LE_ERR?i._showError(i.msgFileNotReadable.replace("{name}",t)):a.code=3D=3D=
=3Da.ABORT_ERR?i._showError(i.msgFilePreviewAborted.replace("{name}",t)):=
i._showError(i.msgFilePreviewError.replace("{name}",t))},_addError:functi=
on(e){var =
t=3Dthis,i=3Dt.$errorContainer;e&amp;&amp;i.length&amp;&amp;(i.html(t.err=
orCloseButton+e),c(i.find(".kv-error-close"),"click",function(){i.fadeOut=
("slow")}))},_resetErrors:function(e){var =
t=3Dthis,i=3Dt.$errorContainer;t.isError=3D!1,t.$container.removeClass("h=
as-error"),i.html(""),e?i.fadeOut("slow"):i.hide()},_showFolderError:func=
tion(e){var =
t,i=3Dthis,a=3Di.$errorContainer;e&amp;&amp;(t=3Di.msgFoldersNotAllowed.r=
eplace(/\{n}/g,e),i._addError(t),v(i.$container,"has-error"),a.fadeIn(800=
),i._raise("filefoldererror",[e,t]))},_showUploadError:function(e,t,i){va=
r =
a=3Dthis,n=3Da.$errorContainer,r=3Di||"fileuploaderror",l=3Dt&amp;&amp;t.=
id?'&lt;li =
data-file-id=3D"'+t.id+'"&gt;'+e+"&lt;/li&gt;":"&lt;li&gt;"+e+"&lt;/li&gt=
;";return =
0=3D=3D=3Dn.find("ul").length?a._addError("&lt;ul&gt;"+l+"&lt;/ul&gt;"):n=
.find("ul").append(l),n.fadeIn(800),a._raise(r,[t,e]),a.$container.remove=
Class("file-input-new"),v(a.$container,"has-error"),!0},_showError:functi=
on(e,t,i){var a=3Dthis,n=3Da.$errorContainer,r=3Di||"fileerror";return =
t=3Dt||{},t.reader=3Da.reader,a._addError(e),n.fadeIn(800),a._raise(r,[t,=
e]),a.isUploadable||a._clearFileInput(),a.$container.removeClass("file-in=
put-new"),v(a.$container,"has-error"),a.$btnUpload.attr("disabled",!0),!0=
},_noFilesError:function(e){var =
t=3Dthis,i=3Dt.minFileCount&gt;1?t.filePlural:t.fileSingle,a=3Dt.msgFiles=
TooLess.replace("{n}",t.minFileCount).replace("{files}",i),n=3Dt.$errorCo=
ntainer;t._addError(a),t.isError=3D!0,t._updateFileDetails(0),n.fadeIn(80=
0),t._raise("fileerror",[e,a]),t._clearFileInput(),v(t.$container,"has-er=
ror")},_parseError:function(t,i,a){var =
n=3Dthis,r=3De.trim(i+""),l=3D"."=3D=3D=3Dr.slice(-1)?"":".",o=3Dvoid =
0!=3D=3Dt.responseJSON&amp;&amp;void =
0!=3D=3Dt.responseJSON.error?t.responseJSON.error:t.responseText;return =
n.cancelling&amp;&amp;n.msgUploadAborted&amp;&amp;(r=3Dn.msgUploadAborted=
),n.showAjaxErrorDetails&amp;&amp;o?(o=3De.trim(o.replace(/\n\s*\n/g,"\n"=
)),o=3Do.length&gt;0?"&lt;pre&gt;"+o+"&lt;/pre&gt;":"",r+=3Dl+o):r+=3Dl,n=
.cancelling=3D!1,a?"&lt;b&gt;"+a+": =
&lt;/b&gt;"+r:r},_parseFileType:function(e){var =
t,i,a,n,r=3Dthis;for(n=3D0;n&lt;ee.length;n+=3D1)if(a=3Dee[n],t=3Dle(a,r.=
fileTypeSettings,ae[a]),i=3Dt(e.type,e.name)?a:"",!ne(i))return =
i;return"other"},_parseFilePreviewIcon:function(t,i){var =
a,n,r=3Dthis,l=3Dr.previewFileIcon;return =
i&amp;&amp;i.indexOf(".")&gt;-1&amp;&amp;(n=3Di.split(".").pop(),r.previe=
wFileIconSettings&amp;&amp;r.previewFileIconSettings[n]&amp;&amp;(l=3Dr.p=
reviewFileIconSettings[n]),r.previewFileExtSettings&amp;&amp;e.each(r.pre=
viewFileExtSettings,function(e,t){return =
r.previewFileIconSettings[e]&amp;&amp;t(n)?void(l=3Dr.previewFileIconSett=
ings[e]):void(a=3D!0)})),t.indexOf("{previewFileIcon}")&gt;-1?t.replace(/=
\{previewFileIconClass}/g,r.previewFileIconClass).replace(/\{previewFileI=
con}/g,l):t},_raise:function(t,i){var a=3Dthis,n=3De.Event(t);if(void =
0!=3D=3Di?a.$element.trigger(n,i):a.$element.trigger(n),n.isDefaultPreven=
ted())return!1;if(!n.result)return =
n.result;switch(t){case"filebatchuploadcomplete":case"filebatchuploadsucc=
ess":case"fileuploaded":case"fileclear":case"filecleared":case"filereset"=
:case"fileerror":case"filefoldererror":case"fileuploaderror":case"filebat=
chuploaderror":case"filedeleteerror":case"filecustomerror":case"filesucce=
ssremove":break;default:a.ajaxAborted=3Dn.result}return!0},_listenFullScr=
een:function(e){var =
t,i,a=3Dthis,n=3Da.$modal;n&amp;&amp;n.length&amp;&amp;(t=3Dn&amp;&amp;n.=
find(".btn-fullscreen"),i=3Dn&amp;&amp;n.find(".btn-borderless"),t.length=
&amp;&amp;i.length&amp;&amp;(t.removeClass("active").attr("aria-pressed",=
"false"),i.removeClass("active").attr("aria-pressed","false"),e?t.addClas=
s("active").attr("aria-pressed","true"):i.addClass("active").attr("aria-p=
ressed","true"),n.hasClass("file-zoom-fullscreen")?a._maximizeZoomDialog(=
):e?a._maximizeZoomDialog():i.removeClass("active").attr("aria-pressed","=
false")))},_listen:function(){var =
t=3Dthis,i=3Dt.$element,a=3Di.closest("form"),n=3Dt.$container;c(i,"chang=
e",e.proxy(t._change,t)),t.showBrowse&amp;&amp;c(t.$btnFile,"click",e.pro=
xy(t._browse,t)),c(a,"reset",e.proxy(t.reset,t)),c(n.find(".fileinput-rem=
ove:not([disabled])"),"click",e.proxy(t.clear,t)),c(n.find(".fileinput-ca=
ncel"),"click",e.proxy(t.cancel,t)),t._initDragDrop(),t.isUploadable||c(a=
,"submit",e.proxy(t._submitForm,t)),c(t.$container.find(".fileinput-uploa=
d"),"click",e.proxy(t._uploadClick,t)),c(e(window),"resize",function(){t.=
_listenFullScreen(screen.width=3D=3D=3Dwindow.innerWidth&amp;&amp;screen.=
height=3D=3D=3Dwindow.innerHeight)}),c(e(document),"webkitfullscreenchang=
e mozfullscreenchange fullscreenchange =
MSFullscreenChange",function(){t._listenFullScreen(ue())}),t._initClickab=
le()},_initClickable:function(){var =
t,i=3Dthis;i.isClickable&amp;&amp;(t=3Di.isUploadable?i.$dropZone:i.$prev=
iew.find(".file-default-preview"),v(t,"clickable"),t.attr("tabindex",-1),=
c(t,"click",function(a){var =
n=3De(a.target);n.parents(".file-preview-thumbnails").length&amp;&amp;!n.=
parents(".file-default-preview").length||(i.$element.trigger("click"),t.b=
lur())}))},_initDragDrop:function(){var =
t=3Dthis,i=3Dt.$dropZone;t.isUploadable&amp;&amp;t.dropZoneEnabled&amp;&a=
mp;t.showPreview&amp;&amp;(c(i,"dragenter =
dragover",e.proxy(t._zoneDragEnter,t)),c(i,"dragleave",e.proxy(t._zoneDra=
gLeave,t)),c(i,"drop",e.proxy(t._zoneDrop,t)),c(e(document),"dragenter =
dragover =
drop",t._zoneDragDropInit))},_zoneDragDropInit:function(e){e.stopPropagat=
ion(),e.preventDefault()},_zoneDragEnter:function(t){var =
i=3Dthis,a=3De.inArray("Files",t.originalEvent.dataTransfer.types)&gt;-1;=
return =
i._zoneDragDropInit(t),i.isDisabled||!a?(t.originalEvent.dataTransfer.eff=
ectAllowed=3D"none",void(t.originalEvent.dataTransfer.dropEffect=3D"none"=
)):void =
v(i.$dropZone,"file-highlighted")},_zoneDragLeave:function(e){var =
t=3Dthis;t._zoneDragDropInit(e),t.isDisabled||t.$dropZone.removeClass("fi=
le-highlighted")},_zoneDrop:function(e){var =
t=3Dthis;e.preventDefault(),t.isDisabled||ne(e.originalEvent.dataTransfer=
.files)||(t._change(e,"dragdrop"),t.$dropZone.removeClass("file-highlight=
ed"))},_uploadClick:function(e){var =
t,i=3Dthis,a=3Di.$container.find(".fileinput-upload"),n=3D!a.hasClass("di=
sabled")&amp;&amp;ne(a.attr("disabled"));if(!e||!e.isDefaultPrevented()){=
if(!i.isUploadable)return =
void(n&amp;&amp;"submit"!=3D=3Da.attr("type")&amp;&amp;(t=3Da.closest("fo=
rm"),t.length&amp;&amp;t.trigger("submit"),e.preventDefault()));e.prevent=
Default(),n&amp;&amp;i.upload()}},_submitForm:function(){var =
e=3Dthis,t=3De.$element,i=3Dt.get(0).files;return =
i&amp;&amp;e.minFileCount&gt;0&amp;&amp;e._getFileCount(i.length)&lt;e.mi=
nFileCount?(e._noFilesError({}),!1):!e._abort({})},_clearPreview:function=
(){var =
e=3Dthis,t=3De.showUploadedThumbs?e.$preview.find(".file-preview-frame:no=
t(.file-preview-success)"):e.$preview.find(".file-preview-frame");t.remov=
e(),e.$preview.find(".file-preview-frame").length&amp;&amp;e.showPreview|=
|e._resetUpload(),e._validateDefaultPreview()},_initSortable:function(){v=
ar =
t,i,a=3Dthis,n=3Da.$preview;window.Sortable&amp;&amp;(t=3Dn.find(".file-i=
nitial-thumbs"),i=3D{handle:".drag-handle-init",dataIdAttr:"data-preview-=
id",draggable:".file-preview-initial",onSort:function(t){var =
i=3Dt.oldIndex,n=3Dt.newIndex;a.initialPreview=3Dme(a.initialPreview,i,n)=
,a.initialPreviewConfig=3Dme(a.initialPreviewConfig,i,n),p.init(a),a._rai=
se("filesorted",{previewId:e(t.item).attr("id"),oldIndex:i,newIndex:n,sta=
ck:a.initialPreviewConfig})}},t.data("sortable")&amp;&amp;t.sortable("des=
troy"),e.extend(!0,i,a.fileActionSettings.dragSettings),t.sortable(i))},_=
initPreview:function(e){var t,i=3Dthis,a=3Di.initialCaption||"";return =
p.count(i.id)?(t=3Dp.out(i.id),a=3De&amp;&amp;i.initialCaption?i.initialC=
aption:t.caption,i.$preview.html(t.content),i._setCaption(a),i._initSorta=
ble(),void(ne(t.content)||i.$container.removeClass("file-input-new"))):(i=
._clearPreview(),void(e?i._setCaption(a):i._initCaption()))},_getZoomButt=
on:function(e){var =
t=3Dthis,i=3Dt.previewZoomButtonIcons[e],a=3Dt.previewZoomButtonClasses[e=
],n=3D' title=3D"'+(t.previewZoomButtonTitles[e]||"")+'" =
',r=3Dn+("close"=3D=3D=3De?' data-dismiss=3D"modal" =
aria-hidden=3D"true"':"");return"fullscreen"!=3D=3De&amp;&amp;"borderless=
"!=3D=3De&amp;&amp;"toggleheader"!=3D=3De||(r+=3D' =
data-toggle=3D"button" aria-pressed=3D"false" =
autocomplete=3D"off"'),'&lt;button type=3D"button" class=3D"'+a+" =
btn-"+e+'"'+r+"&gt;"+i+"&lt;/button&gt;"},_getModalContent:function(){var=
 e=3Dthis;return =
e._getLayoutTemplate("modal").replace(/\{heading}/g,e.msgZoomModalHeading=
).replace(/\{prev}/g,e._getZoomButton("prev")).replace(/\{next}/g,e._getZ=
oomButton("next")).replace(/\{toggleheader}/g,e._getZoomButton("togglehea=
der")).replace(/\{fullscreen}/g,e._getZoomButton("fullscreen")).replace(/=
\{borderless}/g,e._getZoomButton("borderless")).replace(/\{close}/g,e._ge=
tZoomButton("close"))},_listenModalEvent:function(e){var =
t=3Dthis,i=3Dt.$modal,a=3Dfunction(e){return{sourceEvent:e,previewId:i.da=
ta("previewId"),modal:i}};i.on(e+".bs.modal",function(n){var =
r=3Di.find(".btn-fullscreen"),l=3Di.find(".btn-borderless");t._raise("fil=
ezoom"+e,a(n)),"shown"=3D=3D=3De&amp;&amp;(l.removeClass("active").attr("=
aria-pressed","false"),r.removeClass("active").attr("aria-pressed","false=
"),i.hasClass("file-zoom-fullscreen")&amp;&amp;(t._maximizeZoomDialog(),u=
e()?r.addClass("active").attr("aria-pressed","true"):l.addClass("active")=
.attr("aria-pressed","true")))})},_initZoom:function(){var =
t,a=3Dthis,n=3Da._getLayoutTemplate("modalMain"),r=3D"#"+i;a.$modal=3De(r=
),a.$modal&amp;&amp;a.$modal.length||(t=3De(document.createElement("div")=
).html(n).insertAfter(a.$container),a.$modal=3De("#"+i).insertBefore(t),t=
.remove()),a.$modal.html(a._getModalContent()),a._listenModalEvent("show"=
),a._listenModalEvent("shown"),a._listenModalEvent("hide"),a._listenModal=
Event("hidden"),a._listenModalEvent("loaded")},_initZoomButtons:function(=
){var =
t,i,a=3Dthis,n=3Da.$modal.data("previewId")||"",r=3Da.$preview.find(".fil=
e-preview-frame").toArray(),l=3Dr.length,o=3Da.$modal.find(".btn-prev"),s=
=3Da.$modal.find(".btn-next");l&amp;&amp;(t=3De(r[0]),i=3De(r[l-1]),o.rem=
oveAttr("disabled"),s.removeAttr("disabled"),t.length&amp;&amp;t.attr("id=
")=3D=3D=3Dn&amp;&amp;o.attr("disabled",!0),i.length&amp;&amp;i.attr("id"=
)=3D=3D=3Dn&amp;&amp;s.attr("disabled",!0))},_maximizeZoomDialog:function=
(){var =
t=3Dthis,i=3Dt.$modal,a=3Di.find(".modal-header:visible"),n=3Di.find(".mo=
dal-footer:visible"),r=3Di.find(".modal-body"),l=3De(window).height(),o=3D=
0;i.addClass("file-zoom-fullscreen"),a&amp;&amp;a.length&amp;&amp;(l-=3Da=
.outerHeight(!0)),n&amp;&amp;n.length&amp;&amp;(l-=3Dn.outerHeight(!0)),r=
&amp;&amp;r.length&amp;&amp;(o=3Dr.outerHeight(!0)-r.height(),l-=3Do),i.f=
ind(".kv-zoom-body").height(l)},_resizeZoomDialog:function(e){var =
t=3Dthis,i=3Dt.$modal,a=3Di.find(".btn-fullscreen"),n=3Di.find(".btn-bord=
erless");if(i.hasClass("file-zoom-fullscreen"))fe(!1),e?a.hasClass("activ=
e")||(i.removeClass("file-zoom-fullscreen"),t._resizeZoomDialog(!0),n.has=
Class("active")&amp;&amp;n.removeClass("active").attr("aria-pressed","fal=
se")):a.hasClass("active")?a.removeClass("active").attr("aria-pressed","f=
alse"):(i.removeClass("file-zoom-fullscreen"),t.$modal.find(".kv-zoom-bod=
y").css("height",t.zoomModalHeight));else{if(!e)return void =
t._maximizeZoomDialog();fe(!0)}i.focus()},_setZoomContent:function(t,i){v=
ar =
a,n,r,l,o,s,d,p,u=3Dthis,f=3Dt.attr("id"),m=3Du.$modal,g=3Dm.find(".btn-p=
rev"),h=3Dm.find(".btn-next"),w=3Dm.find(".btn-fullscreen"),b=3Dm.find(".=
btn-borderless"),_=3Dm.find(".btn-toggleheader");n=3Dt.data("template")||=
"generic",a=3Dt.find(".kv-file-content"),r=3Da.length?a.html():"",l=3Dt.f=
ind(".file-footer-caption").text()||"",m.find(".kv-zoom-title").html(l),o=
=3Dm.find(".kv-zoom-body"),i?(p=3Do.clone().insertAfter(o),o.html(r).hide=
(),p.fadeOut("fast",function(){o.fadeIn("fast"),p.remove()})):o.html(r),d=
=3Du.previewZoomSettings[n],d&amp;&amp;(s=3Do.find(".kv-preview-data"),v(=
s,"file-zoom-detail"),e.each(d,function(e,t){s.css(e,t),(s.attr("width")&=
amp;&amp;"width"=3D=3D=3De||s.attr("height")&amp;&amp;"height"=3D=3D=3De)=
&amp;&amp;s.removeAttr(e)})),m.data("previewId",f),c(g,"click",function()=
{u._zoomSlideShow("prev",f)}),c(h,"click",function(){u._zoomSlideShow("ne=
xt",f)}),c(w,"click",function(){u._resizeZoomDialog(!0)}),c(b,"click",fun=
ction(){u._resizeZoomDialog(!1)}),c(_,"click",function(){var =
e,t=3Dm.find(".modal-header"),i=3Dm.find(".modal-body =
.floating-buttons"),a=3Dt.find(".kv-zoom-actions"),n=3Dfunction(e){var =
i=3Du.$modal.find(".kv-zoom-body"),a=3Du.zoomModalHeight;m.hasClass("file=
-zoom-fullscreen")&amp;&amp;(a=3Di.outerHeight(!0),e||(a-=3Dt.outerHeight=
(!0))),i.css("height",e?a+e:a)};t.is(":visible")?(e=3Dt.outerHeight(!0),t=
.slideUp("slow",function(){a.find(".btn").appendTo(i),n(e)})):(i.find(".b=
tn").appendTo(a),t.slideDown("slow",function(){n()})),m.focus()}),c(m,"ke=
ydown",function(e){var =
t=3De.which||e.keyCode;37!=3D=3Dt||g.attr("disabled")||u._zoomSlideShow("=
prev",f),39!=3D=3Dt||h.attr("disabled")||u._zoomSlideShow("next",f)})},_z=
oomPreview:function(e){var t,i=3Dthis;if(!e.length)throw"Cannot zoom to =
detailed =
preview!";i.$modal.html(i._getModalContent()),t=3De.closest(".file-previe=
w-frame"),i._setZoomContent(t),i.$modal.modal("show"),i._initZoomButtons(=
)},_zoomSlideShow:function(t,i){var =
a,n,r,l=3Dthis,o=3Dl.$modal.find(".kv-zoom-actions =
.btn-"+t),s=3Dl.$preview.find(".file-preview-frame").toArray(),d=3Ds.leng=
th;if(!o.attr("disabled")){for(n=3D0;d&gt;n;n++)if(e(s[n]).attr("id")=3D=3D=
=3Di){r=3D"prev"=3D=3D=3Dt?n-1:n+1;break}0&gt;r||r&gt;=3Dd||!s[r]||(a=3De=
(s[r]),a.length&amp;&amp;l._setZoomContent(a,!0),l._initZoomButtons(),l._=
raise("filezoom"+t,{previewId:i,modal:l.$modal}))}},_initZoomButton:funct=
ion(){var t=3Dthis;t.$preview.find(".kv-file-zoom").each(function(){var =
i=3De(this);c(i,"click",function(){t._zoomPreview(i)})})},_initPreviewAct=
ions:function(){var =
t=3Dthis,i=3Dt.deleteExtraData||{},a=3Dfunction(){var =
e=3Dt.isUploadable?p.count(t.id):t.$element.get(0).files.length;0!=3D=3Dt=
.$preview.find(".kv-file-remove").length||e||(t.reset(),t.initialCaption=3D=
"")};t._initZoomButton(),t.$preview.find(".kv-file-remove").each(function=
(){var =
n=3De(this),r=3Dn.data("url")||t.deleteUrl,l=3Dn.data("key");if(!ne(r)&am=
p;&amp;void 0!=3D=3Dl){var =
o,s,d,u,f=3Dn.closest(".file-preview-frame"),m=3Dp.data[t.id],g=3Df.data(=
"fileindex");g=3DparseInt(g.replace("init_","")),d=3Dne(m.config)&amp;&am=
p;ne(m.config[g])?null:m.config[g],u=3Dne(d)||ne(d.extra)?i:d.extra,"func=
tion"=3D=3Dtypeof =
u&amp;&amp;(u=3Du()),s=3D{id:n.attr("id"),key:l,extra:u},o=3De.extend(!0,=
{},{url:r,type:"POST",dataType:"json",data:e.extend(!0,{},{key:l},u),befo=
reSend:function(e){t.ajaxAborted=3D!1,t._raise("filepredelete",[l,e,u]),t=
.ajaxAborted?e.abort():(v(f,"file-uploading"),v(n,"disabled"))},success:f=
unction(e,i,r){var o,d;return =
ne(e)||ne(e.error)?(p.unset(t.id,g),o=3Dp.count(t.id),d=3Do&gt;0?t._getMs=
gSelected(o):"",t._raise("filedeleted",[l,r,u]),t._setCaption(d),f.remove=
Class("file-uploading").addClass("file-deleted"),void =
f.fadeOut("slow",function(){t._clearObjects(f),f.remove(),a(),o||0!=3D=3D=
t.getFileStack().length||(t._setCaption(""),t.reset())})):(s.jqXHR=3Dr,s.=
response=3De,t._showError(e.error,s,"filedeleteerror"),=0A=
f.removeClass("file-uploading"),n.removeClass("disabled"),void =
a())},error:function(e,i,n){var =
r=3Dt._parseError(e,n);s.jqXHR=3De,s.response=3D{},t._showError(r,s,"file=
deleteerror"),f.removeClass("file-uploading"),a()}},t.ajaxDeleteSettings)=
,c(n,"click",function(){return t._validateMinCount()?void =
e.ajax(o):!1})}})},_clearObjects:function(t){t.find("video =
audio").each(function(){this.pause(),e(this).remove()}),t.find("img =
object =
div").each(function(){e(this).remove()})},_clearFileInput:function(){var =
t,i,a,n=3Dthis,r=3Dn.$element;ne(r.val())||(n.isIE9||n.isIE10?(t=3Dr.clos=
est("form"),i=3De(document.createElement("form")),a=3De(document.createEl=
ement("div")),r.before(a),t.length?t.after(i):a.after(i),i.append(r).trig=
ger("reset"),a.before(r).remove(),i.remove()):r.val(""),n.fileInputCleare=
d=3D!0)},_resetUpload:function(){var =
e=3Dthis;e.uploadCache=3D{content:[],config:[],tags:[],append:!0},e.uploa=
dCount=3D0,e.uploadStatus=3D{},e.uploadLog=3D[],e.uploadAsyncCount=3D0,e.=
loadedImages=3D[],e.totalImagesCount=3D0,e.$btnUpload.removeAttr("disable=
d"),e._setProgress(0),v(e.$progress,"hide"),e._resetErrors(!1),e.ajaxAbor=
ted=3D!1,e.ajaxRequests=3D[],e._resetCanvas()},_resetCanvas:function(){va=
r =
e=3Dthis;e.canvas&amp;&amp;e.imageCanvasContext&amp;&amp;e.imageCanvasCon=
text.clearRect(0,0,e.canvas.width,e.canvas.height)},_hasInitialPreview:fu=
nction(){var =
e=3Dthis;return!e.overwriteInitial&amp;&amp;p.count(e.id)},_resetPreview:=
function(){var =
e,t,i=3Dthis;p.count(i.id)?(e=3Dp.out(i.id),i.$preview.html(e.content),t=3D=
i.initialCaption?i.initialCaption:e.caption,i._setCaption(t)):(i._clearPr=
eview(),i._initCaption()),i.showPreview&amp;&amp;(i._initZoom(),i._initSo=
rtable())},_clearDefaultPreview:function(){var =
e=3Dthis;e.$preview.find(".file-default-preview").remove()},_validateDefa=
ultPreview:function(){var =
e=3Dthis;e.showPreview&amp;&amp;!ne(e.defaultPreviewContent)&amp;&amp;(e.=
$preview.html('&lt;div =
class=3D"file-default-preview"&gt;'+e.defaultPreviewContent+"&lt;/div&gt;=
"),e.$container.removeClass("file-input-new"),e._initClickable())},_reset=
PreviewThumbs:function(e){var t,i=3Dthis;return =
e?(i._clearPreview(),void =
i.clearStack()):void(i._hasInitialPreview()?(t=3Dp.out(i.id),i.$preview.h=
tml(t.content),i._setCaption(t.caption),i._initPreviewActions()):i._clear=
Preview())},_getLayoutTemplate:function(e){var =
t=3Dthis,i=3Dle(e,t.layoutTemplates,Y[e]);return =
ne(t.customLayoutTags)?i:de(i,t.customLayoutTags)},_getPreviewTemplate:fu=
nction(e){var t=3Dthis,i=3Dle(e,t.previewTemplates,J[e]);return =
ne(t.customPreviewTags)?i:de(i,t.customPreviewTags)},_getOutData:function=
(e,t,i){var a=3Dthis;return =
e=3De||{},t=3Dt||{},i=3Di||a.filestack.slice(0)||{},{form:a.formdata,file=
s:i,filenames:a.filenames,extra:a._getExtraData(),response:t,reader:a.rea=
der,jqXHR:e}},_getMsgSelected:function(e){var =
t=3Dthis,i=3D1=3D=3D=3De?t.fileSingle:t.filePlural;return =
t.msgSelected.replace("{n}",e).replace("{files}",i)},_getThumbs:function(=
e){return =
e=3De||"",this.$preview.find(".file-preview-frame:not(.file-preview-initi=
al)"+e)},_getExtraData:function(e,t){var =
i=3Dthis,a=3Di.uploadExtraData;return"function"=3D=3Dtypeof =
i.uploadExtraData&amp;&amp;(a=3Di.uploadExtraData(e,t)),a},_initXhr:funct=
ion(e,t,i){var a=3Dthis;return =
e.upload&amp;&amp;e.upload.addEventListener("progress",function(e){var =
n=3D0,r=3De.loaded||e.position,l=3De.total;e.lengthComputable&amp;&amp;(n=
=3DMath.ceil(r/l*100)),t?a._setAsyncUploadStatus(t,n,i):a._setProgress(Ma=
th.ceil(n))},!1),e},_ajaxSubmit:function(t,i,a,n,r,l){var =
o,s=3Dthis;s._raise("filepreajax",[r,l]),s._uploadExtra(r,l),o=3De.extend=
(!0,{},{xhr:function(){var t=3De.ajaxSettings.xhr();return =
s._initXhr(t,r,s.getFileStack().length)},url:s.uploadUrl,type:"POST",data=
Type:"json",data:s.formdata,cache:!1,processData:!1,contentType:!1,before=
Send:t,success:i,complete:a,error:n},s.ajaxSettings),s.ajaxRequests.push(=
e.ajax(o))},_initUploadSuccess:function(t,i,a){var =
n,r,l,o,s,d,c,u,f=3Dthis;f.showPreview&amp;&amp;"object"=3D=3Dtypeof =
t&amp;&amp;!e.isEmptyObject(t)&amp;&amp;void =
0!=3D=3Dt.initialPreview&amp;&amp;t.initialPreview.length&gt;0&amp;&amp;(=
f.hasInitData=3D!0,s=3Dt.initialPreview||[],d=3Dt.initialPreviewConfig||[=
],c=3Dt.initialPreviewThumbTags||[],n=3D!(void =
0!=3D=3Dt.append&amp;&amp;!t.append),s.length&gt;0&amp;&amp;!re(s)&amp;&a=
mp;(s=3Ds.split(f.initialPreviewDelimiter)),f.overwriteInitial=3D!1,f.ini=
tialPreview.concat(s),f.initialPreviewThumbTags.concat(c),f.initialPrevie=
wConfig.concat(d),void =
0!=3D=3Di?a?(u=3Di.attr("data-fileindex"),f.uploadCache.content[u]=3Ds[0]=
,f.uploadCache.config[u]=3Dd[0],f.uploadCache.tags[u]=3Dc[0],f.uploadCach=
e.append=3Dn):(l=3Dp.add(f.id,s,d[0],c[0],n),r=3Dp.get(f.id,l,!1),o=3De(r=
).hide(),i.after(o).fadeOut("slow",function(){o.fadeIn("slow").css("displ=
ay:inline-block"),f._initPreviewActions(),f._clearFileInput(),i.remove()}=
)):(p.set(f.id,s,d,c,n),f._initPreview(),f._initPreviewActions()))},_init=
SuccessThumbs:function(){var =
t=3Dthis;t.showPreview&amp;&amp;t._getThumbs(".file-preview-success").eac=
h(function(){var =
i=3De(this),a=3Di.find(".kv-file-remove");a.removeAttr("disabled"),c(a,"c=
lick",function(){var =
e=3Dt._raise("filesuccessremove",[i.attr("id"),i.data("fileindex")]);ce(i=
),e!=3D=3D!1&amp;&amp;i.fadeOut("slow",function(){i.remove(),t.$preview.f=
ind(".file-preview-frame").length||t.reset()})})})},_checkAsyncComplete:f=
unction(){var =
t,i,a=3Dthis;for(i=3D0;i&lt;a.filestack.length;i++)if(a.filestack[i]&amp;=
&amp;(t=3Da.previewInitId+"-"+i,-1=3D=3D=3De.inArray(t,a.uploadLog)))retu=
rn!1;return =
a.uploadAsyncCount=3D=3D=3Da.uploadLog.length},_uploadExtra:function(t,i)=
{var =
a=3Dthis,n=3Da._getExtraData(t,i);0!=3D=3Dn.length&amp;&amp;e.each(n,func=
tion(e,t){a.formdata.append(e,t)})},_uploadSingle:function(t,i,a){var =
n,r,l,o,s,d,c,u,f,m,g=3Dthis,h=3Dg.getFileStack().length,w=3Dnew =
FormData,b=3Dg.previewInitId+"-"+t,_=3Dg.filestack.length&gt;0||!e.isEmpt=
yObject(g.uploadExtraData),C=3D{id:b,index:t};g.formdata=3Dw,g.showPrevie=
w&amp;&amp;(r=3De("#"+b+":not(.file-preview-initial)"),o=3Dr.find(".kv-fi=
le-upload"),s=3Dr.find(".kv-file-remove"),e("#"+b).find(".file-thumb-prog=
ress").removeClass("hide")),0=3D=3D=3Dh||!_||o&amp;&amp;o.hasClass("disab=
led")||g._abort(C)||(m=3Dfunction(e,t){g.updateStack(e,void =
0),g.uploadLog.push(t),g._checkAsyncComplete()&amp;&amp;(g.fileBatchCompl=
eted=3D!0)},l=3Dfunction(){var =
e=3Dg.uploadCache;g.fileBatchCompleted&amp;&amp;setTimeout(function(){g.s=
howPreview&amp;&amp;(p.set(g.id,e.content,e.config,e.tags,e.append),g.has=
InitData&amp;&amp;(g._initPreview(),g._initPreviewActions())),g.unlock(),=
g._clearFileInput(),g._raise("filebatchuploadcomplete",[g.filestack,g._ge=
tExtraData()]),g.uploadCount=3D0,g.uploadStatus=3D{},g.uploadLog=3D[],g._=
setProgress(100)},100)},d=3Dfunction(i){n=3Dg._getOutData(i),g.fileBatchC=
ompleted=3D!1,g.showPreview&amp;&amp;(r.hasClass("file-preview-success")|=
|(g._setThumbStatus(r,"Loading"),v(r,"file-uploading")),o.attr("disabled"=
,!0),s.attr("disabled",!0)),a||g.lock(),g._raise("filepreupload",[n,b,t])=
,e.extend(!0,C,n),g._abort(C)&amp;&amp;(i.abort(),g._setProgressCancelled=
())},c=3Dfunction(i,l,s){n=3Dg._getOutData(s,i),e.extend(!0,C,n),setTimeo=
ut(function(){ne(i)||ne(i.error)?(g.showPreview&amp;&amp;(g._setThumbStat=
us(r,"Success"),o.hide(),g._initUploadSuccess(i,r,a)),g._raise("fileuploa=
ded",[n,b,t]),a?m(t,b):g.updateStack(t,void =
0)):(g._showUploadError(i.error,C),g._setPreviewError(r,t),a&amp;&amp;m(t=
,b))},100)},u=3Dfunction(){setTimeout(function(){g.showPreview&amp;&amp;(=
o.removeAttr("disabled"),s.removeAttr("disabled"),r.removeClass("file-upl=
oading")),a?l():(g.unlock(!1),g._clearFileInput()),g._initSuccessThumbs()=
},100)},f=3Dfunction(n,l,o){var =
s=3Dg._parseError(n,o,a?i[t].name:null);setTimeout(function(){a&amp;&amp;=
m(t,b),g.uploadStatus[b]=3D100,g._setPreviewError(r,t),e.extend(!0,C,g._g=
etOutData(n)),g._showUploadError(s,C)},100)},w.append(g.uploadFileAttr,i[=
t],g.filenames[t]),w.append("file_id",t),g._ajaxSubmit(d,c,u,f,b,t))},_up=
loadBatch:function(){var =
t,i,a,n,r,l=3Dthis,o=3Dl.filestack,s=3Do.length,d=3D{},c=3Dl.filestack.le=
ngth&gt;0||!e.isEmptyObject(l.uploadExtraData);l.formdata=3Dnew =
FormData,0!=3D=3Ds&amp;&amp;c&amp;&amp;!l._abort(d)&amp;&amp;(r=3Dfunctio=
n(){e.each(o,function(e){l.updateStack(e,void =
0)}),l._clearFileInput()},t=3Dfunction(t){l.lock();var =
i=3Dl._getOutData(t);l.showPreview&amp;&amp;l._getThumbs().each(function(=
){var =
t=3De(this),i=3Dt.find(".kv-file-upload"),a=3Dt.find(".kv-file-remove");t=
.hasClass("file-preview-success")||(l._setThumbStatus(t,"Loading"),v(t,"f=
ile-uploading")),i.attr("disabled",!0),a.attr("disabled",!0)}),l._raise("=
filebatchpreupload",[i]),l._abort(i)&amp;&amp;(t.abort(),l._setProgressCa=
ncelled())},i=3Dfunction(t,i,a){var =
n=3Dl._getOutData(a,t),o=3Dl._getThumbs(),s=3D0,d=3Dne(t)||ne(t.errorkeys=
)?[]:t.errorkeys;ne(t)||ne(t.error)?(l._raise("filebatchuploadsuccess",[n=
]),r(),l.showPreview?(o.each(function(){var =
t=3De(this),i=3Dt.find(".kv-file-upload");t.find(".kv-file-upload").hide(=
),l._setThumbStatus(t,"Success"),t.removeClass("file-uploading"),i.remove=
Attr("disabled")}),l._initUploadSuccess(t)):l.reset()):(l.showPreview&amp=
;&amp;(o.each(function(){var =
t=3De(this),i=3Dt.find(".kv-file-remove"),a=3Dt.find(".kv-file-upload");r=
eturn =
t.removeClass("file-uploading"),a.removeAttr("disabled"),i.removeAttr("di=
sabled"),0=3D=3D=3Dd.length?void =
l._setPreviewError(t):(-1!=3D=3De.inArray(s,d)?l._setPreviewError(t):(t.f=
ind(".kv-file-upload").hide(),l._setThumbStatus(t,"Success"),l.updateStac=
k(s,void 0)),void =
s++)}),l._initUploadSuccess(t)),l._showUploadError(t.error,n,"filebatchup=
loaderror"))},n=3Dfunction(){l._setProgress(100),l.unlock(),l._initSucces=
sThumbs(),l._clearFileInput(),l._raise("filebatchuploadcomplete",[l.files=
tack,l._getExtraData()])},a=3Dfunction(t,i,a){var =
n=3Dl._getOutData(t),r=3Dl._parseError(t,a);l._showUploadError(r,n,"fileb=
atchuploaderror"),l.uploadFileCount=3Ds-1,l.showPreview&amp;&amp;(l._getT=
humbs().each(function(){var =
t=3De(this),i=3Dt.attr("data-fileindex");t.removeClass("file-uploading"),=
void =
0!=3D=3Dl.filestack[i]&amp;&amp;l._setPreviewError(t)}),l._getThumbs().re=
moveClass("file-uploading"),l._getThumbs(" =
.kv-file-upload").removeAttr("disabled"),l._getThumbs(" =
.kv-file-delete").removeAttr("disabled"))},e.each(o,function(e,t){ne(o[e]=
)||l.formdata.append(l.uploadFileAttr,t,l.filenames[e])}),l._ajaxSubmit(t=
,i,n,a))},_uploadExtraOnly:function(){var =
e,t,i,a,n=3Dthis,r=3D{};n.formdata=3Dnew =
FormData,n._abort(r)||(e=3Dfunction(e){n.lock();var =
t=3Dn._getOutData(e);n._raise("filebatchpreupload",[t]),n._setProgress(50=
),r.data=3Dt,r.xhr=3De,n._abort(r)&amp;&amp;(e.abort(),n._setProgressCanc=
elled())},t=3Dfunction(e,t,i){var =
a=3Dn._getOutData(i,e);ne(e)||ne(e.error)?(n._raise("filebatchuploadsucce=
ss",[a]),n._clearFileInput(),n._initUploadSuccess(e)):n._showUploadError(=
e.error,a,"filebatchuploaderror")},i=3Dfunction(){n._setProgress(100),n.u=
nlock(),n._clearFileInput(),n._raise("filebatchuploadcomplete",[n.filesta=
ck,n._getExtraData()])},a=3Dfunction(e,t,i){var =
a=3Dn._getOutData(e),l=3Dn._parseError(e,i);r.data=3Da,n._showUploadError=
(l,a,"filebatchuploaderror")},n._ajaxSubmit(e,t,i,a))},_initFileActions:f=
unction(){var =
t=3Dthis;t.showPreview&amp;&amp;(t._initZoomButton(),t.$preview.find(".kv=
-file-remove").each(function(){var =
i,a,n,r,l=3De(this),o=3Dl.closest(".file-preview-frame"),s=3Do.attr("id")=
,d=3Do.attr("data-fileindex");c(l,"click",function(){return =
r=3Dt._raise("filepreremove",[s,d]),r!=3D=3D!1&amp;&amp;t._validateMinCou=
nt()?(i=3Do.hasClass("file-preview-error"),ce(o),void =
o.fadeOut("slow",function(){t.updateStack(d,void =
0),t._clearObjects(o),o.remove(),s&amp;&amp;i&amp;&amp;t.$errorContainer.=
find('li[data-file-id=3D"'+s+'"]').fadeOut("fast",function(){e(this).remo=
ve(),t._errorsExist()||t._resetErrors()}),t._clearFileInput();var =
r=3Dt.getFileStack(!0),l=3Dp.count(t.id),c=3Dr.length,u=3Dt.showPreview&a=
mp;&amp;t.$preview.find(".file-preview-frame").length;0!=3D=3Dc||0!=3D=3D=
l||u?(a=3Dl+c,n=3Da&gt;1?t._getMsgSelected(a):r[0]?t._getFileNames()[0]:"=
",t._setCaption(n)):t.reset(),t._raise("fileremoved",[s,d])})):!1})}),t.$=
preview.find(".kv-file-upload").each(function(){var =
i=3De(this);c(i,"click",function(){var =
e=3Di.closest(".file-preview-frame"),a=3De.attr("data-fileindex");e.hasCl=
ass("file-preview-error")||t._uploadSingle(a,t.filestack,!1)})}))},_hideF=
ileIcon:function(){this.overwriteInitial&amp;&amp;this.$captionContainer.=
find(".kv-caption-icon").hide()},_showFileIcon:function(){this.$captionCo=
ntainer.find(".kv-caption-icon").show()},_getSize:function(e){var =
t=3DparseFloat(e);if(null=3D=3D=3De||isNaN(t))return"";var =
i,a,n,r=3Dthis,l=3Dr.fileSizeGetter;return"function"=3D=3Dtypeof =
l?n=3Dl(e):(i=3DMath.floor(Math.log(t)/Math.log(1024)),a=3D["B","KB","MB"=
,"GB","TB","PB","EB","ZB","YB"],n=3D1*(t/Math.pow(1024,i)).toFixed(2)+" =
"+a[i]),r._getLayoutTemplate("size").replace("{sizeText}",n)},_generatePr=
eviewTemplate:function(e,t,i,a,n,r,l,o,s,d){var =
c,p,u=3Dthis,f=3Du._getPreviewTemplate(e),m=3Do||"",g=3Dle(e,u.previewSet=
tings,ie[e]),v=3Du.slug(i),h=3Ds||u._renderFileFooter(v,l,g.width,r);retu=
rn =
d=3Dd||n.slice(n.lastIndexOf("-")+1),f=3Du._parseFilePreviewIcon(f,i),"te=
xt"=3D=3D=3De||"html"=3D=3D=3De?(p=3D"text"=3D=3D=3De?se(t):t,c=3Df.repla=
ce(/\{previewId}/g,n).replace(/\{caption}/g,v).replace(/\{width}/g,g.widt=
h).replace(/\{height}/g,g.height).replace(/\{frameClass}/g,m).replace(/\{=
cat}/g,a).replace(/\{footer}/g,h).replace(/\{fileindex}/g,d).replace(/\{d=
ata}/g,p).replace(/\{template}/g,e)):c=3Df.replace(/\{previewId}/g,n).rep=
lace(/\{caption}/g,v).replace(/\{frameClass}/g,m).replace(/\{type}/g,a).r=
eplace(/\{fileindex}/g,d).replace(/\{width}/g,g.width).replace(/\{height}=
/g,g.height).replace(/\{footer}/g,h).replace(/\{data}/g,t).replace(/\{tem=
plate}/g,e),c},_previewDefault:function(t,i,a){var =
n=3Dthis,r=3Dn.$preview,o=3Dr.find(".file-live-thumbs");if(n.showPreview)=
{var =
s,d=3Dt?t.name:"",c=3Dt?t.type:"",p=3Da=3D=3D=3D!0&amp;&amp;!n.isUploadab=
le,u=3Dl.createObjectURL(t);n._clearDefaultPreview(),s=3Dn._generatePrevi=
ewTemplate("other",u,d,c,i,p,t.size),o.length||(o=3De(document.createElem=
ent("div")).addClass("file-live-thumbs").appendTo(r)),o.append("\n"+s),a=3D=
=3D=3D!0&amp;&amp;n.isUploadable&amp;&amp;n._setThumbStatus(e("#"+i),"Err=
or")}},_previewFile:function(t,i,a,n,r){if(this.showPreview){var =
l,o=3Dthis,s=3Do._parseFileType(i),d=3Di?i.name:"",c=3Do.slug(d),p=3Do.al=
lowedPreviewTypes,u=3Do.allowedPreviewMimeTypes,f=3Do.$preview,m=3Dp&amp;=
&amp;p.indexOf(s)&gt;=3D0,g=3Df.find(".file-live-thumbs"),v=3D"text"=3D=3D=
=3Ds||"html"=3D=3D=3Ds||"image"=3D=3D=3Ds?a.target.result:r,h=3Du&amp;&am=
p;-1!=3D=3Du.indexOf(i.type);g.length||(g=3De(document.createElement("div=
")).addClass("file-live-thumbs").appendTo(f)),"html"=3D=3D=3Ds&amp;&amp;o=
.purifyHtml&amp;&amp;window.DOMPurify&amp;&amp;(v=3Dwindow.DOMPurify.sani=
tize(v)),m||h?(l=3Do._generatePreviewTemplate(s,v,d,i.type,n,!1,i.size),o=
._clearDefaultPreview(),g.append("\n"+l),o._validateImage(t,n,c,i.type)):=
o._previewDefault(i,n),o._initSortable()}},_slugDefault:function(e){retur=
n =
ne(e)?"":String(e).replace(/[\-\[\]\/\{}:;#%=3D\(\)\*\+\?\\\^\$\|&lt;&gt;=
&amp;"']/g,"_")},_readFiles:function(t){this.reader=3Dnew FileReader;var =
i,a=3Dthis,n=3Da.$element,r=3Da.$preview,s=3Da.reader,d=3Da.$previewConta=
iner,c=3Da.$previewStatus,p=3Da.msgLoading,u=3Da.msgProgress,f=3Da.previe=
wInitId,m=3Dt.length,g=3Da.fileTypeSettings,v=3Da.filestack.length,h=3Da.=
maxFilePreviewSize&amp;&amp;parseFloat(a.maxFilePreviewSize),w=3Dr.length=
&amp;&amp;(!h||isNaN(h)),b=3Dfunction(n,r,l,o){var =
s=3De.extend(!0,{},a._getOutData({},{},t),{id:l,index:o}),d=3D{id:l,index=
:o,file:r,files:t};return =
a._previewDefault(r,l,!0),a.isUploadable&amp;&amp;a.addToStack(void =
0),setTimeout(function(){i(o+1)},100),a._initFileActions(),a.removeFromPr=
eviewOnError&amp;&amp;e("#"+l).remove(),a.isUploadable?a._showUploadError=
(n,s):a._showError(n,d)};a.loadedImages=3D[],a.totalImagesCount=3D0,e.eac=
h(t,function(e,t){var =
i=3Da.fileTypeSettings.image||ae.image;i&amp;&amp;i(t.type)&amp;&amp;a.to=
talImagesCount++}),i=3Dfunction(e){if(ne(n.attr("multiple"))&amp;&amp;(m=3D=
1),e&gt;=3Dm)return =
a.isUploadable&amp;&amp;a.filestack.length&gt;0?a._raise("filebatchselect=
ed",[a.getFileStack()]):a._raise("filebatchselected",[t]),d.removeClass("=
file-thumb-loading"),void c.html("");var =
_,C,x,y,T,F,k,E=3Dv+e,$=3Df+"-"+E,S=3Dt[e],I=3Da.slug(S.name),P=3D(S.size=
||0)/1e3,D=3D"",z=3Dl.createObjectURL(S),A=3D0,U=3Da.allowedFileTypes,j=3D=
ne(U)?"":U.join(", "),L=3Da.allowedFileExtensions,Z=3Dne(L)?"":L.join(", =
");if(ne(L)||(D=3Dnew =
RegExp("\\.("+L.join("|")+")$","i")),P=3DP.toFixed(2),a.maxFileSize&gt;0&=
amp;&amp;P&gt;a.maxFileSize)return =
T=3Da.msgSizeTooLarge.replace("{name}",I).replace("{size}",P).replace("{m=
axSize}",a.maxFileSize),void(a.isError=3Db(T,S,$,e));if(!ne(U)&amp;&amp;r=
e(U)){for(y=3D0;y&lt;U.length;y+=3D1)F=3DU[y],x=3Dg[F],k=3Dvoid =
0!=3D=3Dx&amp;&amp;x(S.type,I),A+=3Dne(k)?0:k.length;if(0=3D=3D=3DA)retur=
n =
T=3Da.msgInvalidFileType.replace("{name}",I).replace("{types}",j),void(a.=
isError=3Db(T,S,$,e))}return =
0!=3D=3DA||ne(L)||!re(L)||ne(D)||(k=3Do(I,D),A+=3Dne(k)?0:k.length,0!=3D=3D=
A)?a.showPreview?!w&amp;&amp;P&gt;h?(d.addClass("file-thumb-loading"),a._=
previewDefault(S,$),a._initFileActions(),a._updateFileDetails(m),void =
i(e+1)):(r.length&amp;&amp;void =
0!=3D=3DFileReader?(c.html(p.replace("{index}",e+1).replace("{files}",m))=
,d.addClass("file-thumb-loading"),s.onerror=3Dfunction(e){a._errorHandler=
(e,I)},s.onload=3Dfunction(t){a._previewFile(e,S,t,$,z),a._initFileAction=
s()},s.onloadend=3Dfunction(){T=3Du.replace("{index}",e+1).replace("{file=
s}",m).replace("{percent}",50).replace("{name}",I),setTimeout(function(){=
c.html(T),a._updateFileDetails(m),i(e+1)},100),a._raise("fileloaded",[S,$=
,e,s])},s.onprogress=3Dfunction(t){if(t.lengthComputable){var =
i=3Dt.loaded/t.total*100,a=3DMath.ceil(i);T=3Du.replace("{index}",e+1).re=
place("{files}",m).replace("{percent}",a).replace("{name}",I),setTimeout(=
function(){c.html(T)},100)}},_=3Dle("text",g,ae.text),C=3Dle("image",g,ae=
.image),_(S.type,I)?s.readAsText(S,a.textEncoding):C(S.type,I)?s.readAsDa=
taURL(S):s.readAsArrayBuffer(S)):(a._previewDefault(S,$),setTimeout(funct=
ion(){i(e+1),a._updateFileDetails(m)},100),a._raise("fileloaded",[S,$,e,s=
])),void =
a.addToStack(S)):(a.addToStack(S),setTimeout(function(){i(e+1)},100),void=
 =
a._raise("fileloaded",[S,$,e,s])):(T=3Da.msgInvalidFileExtension.replace(=
"{name}",I).replace("{extensions}",Z),void(a.isError=3Db(T,S,$,e)))},i(0)=
,a._updateFileDetails(m,!1)},_updateFileDetails:function(e){var =
t=3Dthis,i=3Dt.$element,a=3Dt.getFileStack(),n=3Ds(9)&amp;&amp;pe(i.val()=
)||i[0].files[0]&amp;&amp;i[0].files[0].name||a.length&amp;&amp;a[0].name=
||"",r=3Dt.slug(n),l=3Dt.isUploadable?a.length:e,o=3Dp.count(t.id)+l,d=3D=
l&gt;1?t._getMsgSelected(o):r;t.isError?(t.$previewContainer.removeClass(=
"file-thumb-loading"),t.$previewStatus.html(""),t.$captionContainer.find(=
".kv-caption-icon").hide()):t._showFileIcon(),t._setCaption(d,t.isError),=
t.$container.removeClass("file-input-new =
file-input-ajax-new"),1=3D=3D=3Darguments.length&amp;&amp;t._raise("files=
elect",[e,r]),p.count(t.id)&amp;&amp;t._initPreviewActions()},_setThumbSt=
atus:function(e,t){var i=3Dthis;if(i.showPreview){var =
a=3D"indicator"+t,n=3Da+"Title",r=3D"file-preview-"+t.toLowerCase(),l=3De=
.find(".file-upload-indicator"),o=3Di.fileActionSettings;e.removeClass("f=
ile-preview-success file-preview-error =
file-preview-loading"),"Error"=3D=3D=3Dt&amp;&amp;e.find(".kv-file-upload=
").attr("disabled",!0),"Success"=3D=3D=3Dt&amp;&amp;(e.find(".file-drag-h=
andle").remove(),l.css("margin-left",0)),l.html(o[a]),l.attr("title",o[n]=
),e.addClass(r)}},_setProgressCancelled:function(){var =
e=3Dthis;e._setProgress(100,e.$progress,e.msgCancelled)},_setProgress:fun=
ction(e,t,i){var =
a=3Dthis,n=3DMath.min(e,100),r=3D100&gt;n?a.progressTemplate:i?a.progress=
ErrorTemplate:a.progressCompleteTemplate;t=3Dt||a.$progress,ne(r)||(t.htm=
l(r.replace(/\{percent}/g,n)),i&amp;&amp;t.find('[role=3D"progressbar"]')=
.html(i))},_setFileDropZoneTitle:function(){var =
e,t=3Dthis,i=3Dt.$container.find(".file-drop-zone"),a=3Dt.dropZoneTitle;t=
.isClickable&amp;&amp;(e=3Dne(t.$element.attr("multiple"))?t.fileSingle:t=
.filePlural,a+=3Dt.dropZoneClickTitle.replace("{files}",e)),i.find("."+t.=
dropZoneTitleClass).remove(),t.isUploadable&amp;&amp;t.showPreview&amp;&a=
mp;0!=3D=3Di.length&amp;&amp;!(t.getFileStack().length&gt;0)&amp;&amp;t.d=
ropZoneEnabled&amp;&amp;(0=3D=3D=3Di.find(".file-preview-frame").length&a=
mp;&amp;ne(t.defaultPreviewContent)&amp;&amp;i.prepend('&lt;div =
class=3D"'+t.dropZoneTitleClass+'"&gt;'+a+"&lt;/div&gt;"),t.$container.re=
moveClass("file-input-new"),v(t.$container,"file-input-ajax-new"))},_setA=
syncUploadStatus:function(t,i,a){var =
n=3Dthis,r=3D0;n._setProgress(i,e("#"+t).find(".file-thumb-progress")),n.=
uploadStatus[t]=3Di,e.each(n.uploadStatus,function(e,t){r+=3Dt}),n._setPr=
ogress(Math.ceil(r/a))},_validateMinCount:function(){var =
e=3Dthis,t=3De.isUploadable?e.getFileStack().length:e.$element.get(0).fil=
es.length;return =
e.validateInitialCount&amp;&amp;e.minFileCount&gt;0&amp;&amp;e._getFileCo=
unt(t-1)&lt;e.minFileCount?(e._noFilesError({}),!1):!0},_getFileCount:fun=
ction(e){var t=3Dthis,i=3D0;return =
t.validateInitialCount&amp;&amp;!t.overwriteInitial&amp;&amp;(i=3Dp.count=
(t.id),e+=3Di),e},_getFileName:function(e){return =
e&amp;&amp;e.name?this.slug(e.name):void =
0},_getFileNames:function(e){var t=3Dthis;return =
t.filenames.filter(function(t){return e?void 0!=3D=3Dt:void =
0!=3D=3Dt&amp;&amp;null!=3D=3Dt})},_setPreviewError:function(e,t,i){var =
a=3Dthis;t&amp;&amp;a.updateStack(t,i),a.removeFromPreviewOnError?e.remov=
e():a._setThumbStatus(e,"Error")},_checkDimensions:function(e,t,i,a,n,r,l=
){var =
o,s,d,c,p=3Dthis,u=3D"Small"=3D=3D=3Dt?"min":"max",f=3Dp[u+"Image"+r];!ne=
(f)&amp;&amp;i.length&amp;&amp;(d=3Di[0],s=3D"Width"=3D=3D=3Dr?d.naturalW=
idth||d.width:d.naturalHeight||d.height,c=3D"Small"=3D=3D=3Dt?s&gt;=3Df:f=
&gt;=3Ds,c||(o=3Dp["msgImage"+r+t].replace("{name}",n).replace("{size}",f=
),p._showUploadError(o,l),p._setPreviewError(a,e,null)))},_validateImage:=
function(e,t,i,a){var =
n,r,o,s=3Dthis,d=3Ds.$preview,p=3Dd.find("#"+t),u=3Dp.find("img");i=3Di||=
"Untitled",u.length&amp;&amp;c(u,"load",function(){r=3Dp.width(),o=3Dd.wi=
dth(),r&gt;o&amp;&amp;(u.css("width","100%"),p.css("width","97%")),n=3D{i=
nd:e,id:t},s._checkDimensions(e,"Small",u,p,i,"Width",n),s._checkDimensio=
ns(e,"Small",u,p,i,"Height",n),s.resizeImage||(s._checkDimensions(e,"Larg=
e",u,p,i,"Width",n),s._checkDimensions(e,"Large",u,p,i,"Height",n)),s._ra=
ise("fileimageloaded",[t]),s.loadedImages.push({ind:e,img:u,thumb:p,pid:t=
,typ:a}),s._validateAllImages(),l.revokeObjectURL(u.attr("src"))})},_vali=
dateAllImages:function(){var =
e,t,i,a,n,r,l,o=3Dthis,s=3D{};if(o.loadedImages.length=3D=3D=3Do.totalIma=
gesCount&amp;&amp;(o._raise("fileimagesloaded"),o.resizeImage)){for(l=3Do=
.isUploadable?o._showUploadError:o._showError,e=3D0;e&lt;o.loadedImages.l=
ength;e++)t=3Do.loadedImages[e],i=3Dt.img,a=3Dt.thumb,n=3Dt.pid,r=3Dt.ind=
,s=3D{id:n,index:r},o._getResizedImage(i[0],t.typ,n,r)||(l(o.msgImageResi=
zeError,s,"fileimageresizeerror"),o._setPreviewError(a,r));o._raise("file=
imagesresized")}},_getResizedImage:function(e,t,i,a){var =
n,r,l=3Dthis,o=3De.naturalWidth,s=3De.naturalHeight,d=3D1,c=3Dl.maxImageW=
idth||o,p=3Dl.maxImageHeight||s,u=3Do&amp;&amp;s,f=3Dl.imageCanvas,m=3Dl.=
imageCanvasContext;if(!u)return!1;if(o=3D=3D=3Dc&amp;&amp;s=3D=3D=3Dp)ret=
urn!0;t=3Dt||l.resizeDefaultImageType,n=3Do&gt;c,r=3Ds&gt;p,d=3D"width"=3D=
=3D=3Dl.resizePreference?n?c/o:r?p/s:1:r?p/s:n?c/o:1,l._resetCanvas(),o*=3D=
d,s*=3Dd,f.width=3Do,f.height=3Ds;try{return =
m.drawImage(e,0,0,o,s),f.toBlob(function(e){l._raise("fileimageresized",[=
i,a]),l.filestack[a]=3De},t,l.resizeQuality),!0}catch(g){return!1}},_init=
Browse:function(e){var =
t=3Dthis;t.showBrowse?(t.$btnFile=3De.find(".btn-file"),t.$btnFile.append=
(t.$element)):t.$element.hide()},_initCaption:function(){var =
e=3Dthis,t=3De.initialCaption||"";return =
e.overwriteInitial||ne(t)?(e.$caption.html(""),!1):(e._setCaption(t),!0)}=
,_setCaption:function(t,i){var =
a,n,r,l,o=3Dthis,s=3Do.getFileStack();if(o.$caption.length){if(i)a=3De("&=
lt;div&gt;"+o.msgValidationError+"&lt;/div&gt;").text(),r=3Ds.length,l=3D=
r?1=3D=3D=3Dr&amp;&amp;s[0]?o._getFileNames()[0]:o._getMsgSelected(r):o._=
getMsgSelected(o.msgNo),n=3D'&lt;span =
class=3D"'+o.msgValidationErrorClass+'"&gt;'+o.msgValidationErrorIcon+(ne=
(t)?l:t)+"&lt;/span&gt;";else{if(ne(t))return;a=3De("&lt;div&gt;"+t+"&lt;=
/div&gt;").text(),n=3Do._getLayoutTemplate("fileIcon")+a}o.$caption.html(=
n),o.$caption.attr("title",a),o.$captionContainer.find(".file-caption-ell=
ipsis").attr("title",a)}},_createContainer:function(){var =
t=3Dthis,i=3De(document.createElement("div")).attr({"class":"file-input =
file-input-new"}).html(t._renderMain());return =
t.$element.before(i),t._initBrowse(i),t.theme&amp;&amp;i.addClass("theme-=
"+t.theme),i},_refreshContainer:function(){var =
e=3Dthis,t=3De.$container;t.before(e.$element),t.html(e._renderMain()),e.=
_initBrowse(t)},_renderMain:function(){var =
e=3Dthis,t=3De.isUploadable&amp;&amp;e.dropZoneEnabled?" =
file-drop-zone":"file-drop-disabled",i=3De.showClose?e._getLayoutTemplate=
("close"):"",a=3De.showPreview?e._getLayoutTemplate("preview").replace(/\=
{class}/g,e.previewClass).replace(/\{dropClass}/g,t):"",n=3De.isDisabled?=
e.captionClass+" =
file-caption-disabled":e.captionClass,r=3De.captionTemplate.replace(/\{cl=
ass}/g,n+" kv-fileinput-caption");return =
e.mainTemplate.replace(/\{class}/g,e.mainClass+(!e.showBrowse&amp;&amp;e.=
showCaption?" =
no-browse":"")).replace(/\{preview}/g,a).replace(/\{close}/g,i).replace(/=
\{caption}/g,r).replace(/\{upload}/g,e._renderButton("upload")).replace(/=
\{remove}/g,e._renderButton("remove")).replace(/\{cancel}/g,e._renderButt=
on("cancel")).replace(/\{browse}/g,e._renderButton("browse"))},_renderBut=
ton:function(e){var =
t=3Dthis,i=3Dt._getLayoutTemplate("btnDefault"),a=3Dt[e+"Class"],n=3Dt[e+=
"Title"],r=3Dt[e+"Icon"],l=3Dt[e+"Label"],o=3Dt.isDisabled?" =
disabled":"",s=3D"button";switch(e){case"remove":if(!t.showRemove)return"=
";break;case"cancel":if(!t.showCancel)return"";a+=3D" =
hide";break;case"upload":if(!t.showUpload)return"";t.isUploadable&amp;&am=
p;!t.isDisabled?i=3Dt._getLayoutTemplate("btnLink").replace("{href}",t.up=
loadUrl):s=3D"submit";break;case"browse":if(!t.showBrowse)return"";i=3Dt.=
_getLayoutTemplate("btnBrowse");break;default:return""}return =
a+=3D"browse"=3D=3D=3De?" btn-file":" fileinput-"+e+" =
fileinput-"+e+"-button",ne(l)||(l=3D' &lt;span =
class=3D"'+t.buttonLabelClass+'"&gt;'+l+"&lt;/span&gt;"),i.replace("{type=
}",s).replace("{css}",a).replace("{title}",n).replace("{status}",o).repla=
ce("{icon}",r).replace("{label}",l)},_renderThumbProgress:function(){retu=
rn'&lt;div class=3D"file-thumb-progress =
hide"&gt;'+this.progressTemplate.replace(/\{percent}/g,"0")+"&lt;/div&gt;=
"},_renderFileFooter:function(e,t,i,a){var =
n,r=3Dthis,l=3Dr.fileActionSettings,o=3Dl.showRemove,s=3Dl.showDrag,d=3Dl=
.showUpload,c=3Dl.showZoom,p=3Dr._getLayoutTemplate("footer"),u=3Da?l.ind=
icatorError:l.indicatorNew,f=3Da?l.indicatorErrorTitle:l.indicatorNewTitl=
e;return =
t=3Dr._getSize(t),n=3Dr.isUploadable?p.replace(/\{actions}/g,r._renderFil=
eActions(o,d,c,s,!1,!1,!1)).replace(/\{caption}/g,e).replace(/\{size}/g,t=
).replace(/\{width}/g,i).replace(/\{progress}/g,r._renderThumbProgress())=
.replace(/\{indicator}/g,u).replace(/\{indicatorTitle}/g,f):p.replace(/\{=
actions}/g,r._renderFileActions(!1,!1,c,s,!1,!1,!1)).replace(/\{caption}/=
g,e).replace(/\{size}/g,t).replace(/\{width}/g,i).replace(/\{progress}/g,=
"").replace(/\{indicator}/g,u).replace(/\{indicatorTitle}/g,f),n=3Dde(n,r=
.previewThumbTags)},_renderFileActions:function(e,t,i,a,n,r,l,o){if(!(e||=
t||i||a))return"";var s,d=3Dthis,c=3Dr=3D=3D=3D!1?"":' =
data-url=3D"'+r+'"',p=3Dl=3D=3D=3D!1?"":' =
data-key=3D"'+l+'"',u=3D"",f=3D"",m=3D"",g=3D"",v=3Dd._getLayoutTemplate(=
"actions"),h=3Dd.fileActionSettings,w=3Dd.otherActionButtons.replace(/\{d=
ataKey}/g,p),b=3Dn?h.removeClass+" disabled":h.removeClass;return =
t&amp;&amp;(u=3Dd._getLayoutTemplate("actionDelete").replace(/\{removeCla=
ss}/g,b).replace(/\{removeIcon}/g,h.removeIcon).replace(/\{removeTitle}/g=
,h.removeTitle).replace(/\{dataUrl}/g,c).replace(/\{dataKey}/g,p)),e&amp;=
&amp;(f=3Dd._getLayoutTemplate("actionUpload").replace(/\{uploadClass}/g,=
h.uploadClass).replace(/\{uploadIcon}/g,h.uploadIcon).replace(/\{uploadTi=
tle}/g,h.uploadTitle)),i&amp;&amp;(m=3Dd._getLayoutTemplate("actionZoom")=
.replace(/\{zoomClass}/g,h.zoomClass).replace(/\{zoomIcon}/g,h.zoomIcon).=
replace(/\{zoomTitle}/g,h.zoomTitle)),a&amp;&amp;o&amp;&amp;(s=3D"drag-ha=
ndle-init =
"+h.dragClass,g=3Dd._getLayoutTemplate("actionDrag").replace(/\{dragClass=
}/g,s).replace(/\{dragTitle}/g,h.dragTitle).replace(/\{dragIcon}/g,h.drag=
Icon)),v.replace(/\{delete}/g,u).replace(/\{upload}/g,f).replace(/\{zoom}=
/g,m).replace(/\{drag}/g,g).replace(/\{other}/g,w)},_browse:function(e){v=
ar =
t=3Dthis;t._raise("filebrowse"),e&amp;&amp;e.isDefaultPrevented()||(t.isE=
rror&amp;&amp;!t.isUploadable&amp;&amp;t.clear(),t.$captionContainer.focu=
s())},_change:function(t){var =
i=3Dthis,a=3Di.$element;if(!i.isUploadable&amp;&amp;ne(a.val())&amp;&amp;=
i.fileInputCleared)return =
void(i.fileInputCleared=3D!1);i.fileInputCleared=3D!1;var =
n,r,l,o,s,d,c=3Darguments.length&gt;1,u=3Di.isUploadable,f=3D0,m=3Dc?t.or=
iginalEvent.dataTransfer.files:a.get(0).files,g=3Di.filestack.length,v=3D=
ne(a.attr("multiple")),h=3Dv&amp;&amp;g&gt;0,w=3D0,b=3Dfunction(t,a,n,r){=
var =
l=3De.extend(!0,{},i._getOutData({},{},m),{id:n,index:r}),o=3D{id:n,index=
:r,file:a,files:m};return =
i.isUploadable?i._showUploadError(t,l):i._showError(t,o)};if(i.reader=3Dn=
ull,i._resetUpload(),i._hideFileIcon(),i.isUploadable&amp;&amp;i.$contain=
er.find(".file-drop-zone =
."+i.dropZoneTitleClass).remove(),c)for(n=3D[];m[f];)o=3Dm[f],o.type||o.s=
ize%4096!=3D=3D0?n.push(o):w++,f++;else n=3Dvoid =
0=3D=3D=3Dt.target.files?t.target&amp;&amp;t.target.value?[{name:t.target=
.value.replace(/^.+\\/,"")}]:[]:t.target.files;if(ne(n)||0=3D=3D=3Dn.leng=
th)return u||i.clear(),i._showFolderError(w),void =
i._raise("fileselectnone");if(i._resetErrors(),d=3Dn.length,l=3Di._getFil=
eCount(i.isUploadable?i.getFileStack().length+d:d),i.maxFileCount&gt;0&am=
p;&amp;l&gt;i.maxFileCount){if(!i.autoReplace||d&gt;i.maxFileCount)return=
 =
s=3Di.autoReplace&amp;&amp;d&gt;i.maxFileCount?d:l,r=3Di.msgFilesTooMany.=
replace("{m}",i.maxFileCount).replace("{n}",s),i.isError=3Db(r,null,null,=
null),i.$captionContainer.find(".kv-caption-icon").hide(),i._setCaption("=
",!0),void i.$container.removeClass("file-input-new =
file-input-ajax-new");l&gt;i.maxFileCount&amp;&amp;i._resetPreviewThumbs(=
u)}else!u||h?(i._resetPreviewThumbs(!1),h&amp;&amp;i.clearStack()):!u||0!=
=3D=3Dg||p.count(i.id)&amp;&amp;!i.overwriteInitial||i._resetPreviewThumb=
s(!0);i.isPreviewable?i._readFiles(n):i._updateFileDetails(1),i._showFold=
erError(w)},_abort:function(t){var i,a=3Dthis;return =
a.ajaxAborted&amp;&amp;"object"=3D=3Dtypeof a.ajaxAborted&amp;&amp;void =
0!=3D=3Da.ajaxAborted.message?(i=3De.extend(!0,{},a._getOutData(),t),i.ab=
ortData=3Da.ajaxAborted.data||{},i.abortMessage=3Da.ajaxAborted.message,a=
.cancel(),a._setProgress(100,a.$progress,a.msgCancelled),a._showUploadErr=
or(a.ajaxAborted.message,i,"filecustomerror"),!0):!1},_resetFileStack:fun=
ction(){var =
t=3Dthis,i=3D0,a=3D[],n=3D[];t._getThumbs().each(function(){var =
r=3De(this),l=3Dr.attr("data-fileindex"),o=3Dt.filestack[l];-1!=3D=3Dl&am=
p;&amp;(void =
0!=3D=3Do?(a[i]=3Do,n[i]=3Dt._getFileName(o),r.attr({id:t.previewInitId+"=
-"+i,"data-fileindex":i}),i++):r.attr({id:"uploaded-"+oe(),"data-fileinde=
x":"-1"}))}),t.filestack=3Da,t.filenames=3Dn},clearStack:function(){var =
e=3Dthis;return =
e.filestack=3D[],e.filenames=3D[],e.$element},updateStack:function(e,t){v=
ar i=3Dthis;return =
i.filestack[e]=3Dt,i.filenames[e]=3Di._getFileName(t),i.$element},addToSt=
ack:function(e){var t=3Dthis;return =
t.filestack.push(e),t.filenames.push(t._getFileName(e)),t.$element},getFi=
leStack:function(e){var t=3Dthis;return =
t.filestack.filter(function(t){return e?void 0!=3D=3Dt:void =
0!=3D=3Dt&amp;&amp;null!=3D=3Dt})},lock:function(){var e=3Dthis;return =
e._resetErrors(),e.disable(),e.showRemove&amp;&amp;v(e.$container.find(".=
fileinput-remove"),"hide"),e.showCancel&amp;&amp;e.$container.find(".file=
input-cancel").removeClass("hide"),e._raise("filelock",[e.filestack,e._ge=
tExtraData()]),e.$element},unlock:function(e){var t=3Dthis;return void =
0=3D=3D=3De&amp;&amp;(e=3D!0),t.enable(),t.showCancel&amp;&amp;v(t.$conta=
iner.find(".fileinput-cancel"),"hide"),t.showRemove&amp;&amp;t.$container=
.find(".fileinput-remove").removeClass("hide"),e&amp;&amp;t._resetFileSta=
ck(),t._raise("fileunlock",[t.filestack,t._getExtraData()]),t.$element},c=
ancel:function(){var =
t,i=3Dthis,a=3Di.ajaxRequests,n=3Da.length;if(n&gt;0)for(t=3D0;n&gt;t;t+=3D=
1)i.cancelling=3D!0,a[t].abort();return =
i._setProgressCancelled(),i._getThumbs().each(function(){var =
t=3De(this),a=3Dt.attr("data-fileindex");t.removeClass("file-uploading"),=
void =
0!=3D=3Di.filestack[a]&amp;&amp;(t.find(".kv-file-upload").removeClass("d=
isabled").removeAttr("disabled"),t.find(".kv-file-remove").removeClass("d=
isabled").removeAttr("disabled")),i.unlock()}),i.$element},clear:function=
(){var t,i=3Dthis;return =
i.$btnUpload.removeAttr("disabled"),i._getThumbs().find("video,audio,img"=
).each(function(){ce(e(this))}),i._resetUpload(),i.clearStack(),i._clearF=
ileInput(),i._resetErrors(!0),i._raise("fileclear"),i._hasInitialPreview(=
)?(i._showFileIcon(),i._resetPreview(),i._initPreviewActions(),i.$contain=
er.removeClass("file-input-new")):(i._getThumbs().each(function(){i._clea=
rObjects(e(this))}),i.isUploadable&amp;&amp;(p.data[i.id]=3D{}),i.$previe=
w.html(""),t=3D!i.overwriteInitial&amp;&amp;i.initialCaption.length&gt;0?=
i.initialCaption:"",i._setCaption(t),i.$caption.attr("title",""),v(i.$con=
tainer,"file-input-new"),i._validateDefaultPreview()),0=3D=3D=3Di.$contai=
ner.find(".file-preview-frame").length&amp;&amp;(i._initCaption()||i.$cap=
tionContainer.find(".kv-caption-icon").hide()),i._hideFileIcon(),i._raise=
("filecleared"),i.$captionContainer.focus(),i._setFileDropZoneTitle(),i.$=
element},reset:function(){var e=3Dthis;return =
e._resetPreview(),e.$container.find(".fileinput-filename").text(""),e._ra=
ise("filereset"),v(e.$container,"file-input-new"),(e.$preview.find(".file=
-preview-frame").length||e.isUploadable&amp;&amp;e.dropZoneEnabled)&amp;&=
amp;e.$container.removeClass("file-input-new"),e._setFileDropZoneTitle(),=
e.clearStack(),e.formdata=3D{},e.$element},disable:function(){var =
e=3Dthis;return =
e.isDisabled=3D!0,e._raise("filedisabled"),e.$element.attr("disabled","di=
sabled"),e.$container.find(".kv-fileinput-caption").addClass("file-captio=
n-disabled"),e.$container.find(".btn-file, .fileinput-remove, =
.fileinput-upload, .file-preview-frame =
button").attr("disabled",!0),e._initDragDrop(),e.$element;=0A=
},enable:function(){var e=3Dthis;return =
e.isDisabled=3D!1,e._raise("fileenabled"),e.$element.removeAttr("disabled=
"),e.$container.find(".kv-fileinput-caption").removeClass("file-caption-d=
isabled"),e.$container.find(".btn-file, .fileinput-remove, =
.fileinput-upload, .file-preview-frame =
button").removeAttr("disabled"),e._initDragDrop(),e.$element},upload:func=
tion(){var =
t,i,a,n=3Dthis,r=3Dn.getFileStack().length,l=3D{},o=3D!e.isEmptyObject(n.=
_getExtraData());if(n.minFileCount&gt;0&amp;&amp;n._getFileCount(r)&lt;n.=
minFileCount)return void =
n._noFilesError(l);if(n.isUploadable&amp;&amp;!n.isDisabled&amp;&amp;(0!=3D=
=3Dr||o)){if(n._resetUpload(),n.$progress.removeClass("hide"),n.uploadCou=
nt=3D0,n.uploadStatus=3D{},n.uploadLog=3D[],n.lock(),n._setProgress(2),0=3D=
=3D=3Dr&amp;&amp;o)return void =
n._uploadExtraOnly();if(a=3Dn.filestack.length,n.hasInitData=3D!1,!n.uplo=
adAsync)return =
n._uploadBatch(),n.$element;for(i=3Dn._getOutData(),n._raise("filebatchpr=
eupload",[i]),n.fileBatchCompleted=3D!1,n.uploadCache=3D{content:[],confi=
g:[],tags:[],append:!0},n.uploadAsyncCount=3Dn.getFileStack().length,t=3D=
0;a&gt;t;t++)n.uploadCache.content[t]=3Dnull,n.uploadCache.config[t]=3Dnu=
ll,n.uploadCache.tags[t]=3Dnull;for(t=3D0;a&gt;t;t++)void =
0!=3D=3Dn.filestack[t]&amp;&amp;n._uploadSingle(t,n.filestack,!0)}},destr=
oy:function(){var e=3Dthis,i=3De.$container;return =
i.find(".file-drop-zone").off(),e.$element.insertBefore(i).off(t).removeD=
ata(),i.off().remove(),e.$element},refresh:function(t){var =
i=3Dthis,a=3Di.$element;return =
t=3Dt?e.extend(!0,{},i.options,t):i.options,i.destroy(),a.fileinput(t),a.=
val()&amp;&amp;a.trigger("change.fileinput"),a}},e.fn.fileinput=3Dfunctio=
n(t){if(f()||s(9)){var =
i=3DArray.apply(null,arguments),a=3D[];switch(i.shift(),this.each(functio=
n(){var n,r=3De(this),l=3Dr.data("fileinput"),o=3D"object"=3D=3Dtypeof =
t&amp;&amp;t,s=3Do.theme||r.data("theme"),d=3D{},c=3D{},p=3Do.language||r=
.data("language")||"en";l||(s&amp;&amp;(c=3De.fn.fileinputThemes[s]||{}),=
"en"=3D=3D=3Dp||ne(e.fn.fileinputLocales[p])||(d=3De.fn.fileinputLocales[=
p]||{}),n=3De.extend(!0,{},e.fn.fileinput.defaults,c,e.fn.fileinputLocale=
s.en,d,o,r.data()),l=3Dnew =
ge(this,n),r.data("fileinput",l)),"string"=3D=3Dtypeof =
t&amp;&amp;a.push(l[t].apply(l,i))}),a.length){case 0:return this;case =
1:return a[0];default:return =
a}}},e.fn.fileinput.defaults=3D{language:"en",showCaption:!0,showBrowse:!=
0,showPreview:!0,showRemove:!0,showUpload:!0,showCancel:!0,showClose:!0,s=
howUploadedThumbs:!0,browseOnZoneClick:!1,autoReplace:!1,previewClass:"",=
captionClass:"",mainClass:"file-caption-main",mainTemplate:null,purifyHtm=
l:!0,fileSizeGetter:null,initialCaption:"",initialPreview:[],initialPrevi=
ewDelimiter:"*$$*",initialPreviewAsData:!1,initialPreviewFileType:"image"=
,initialPreviewConfig:[],initialPreviewThumbTags:[],previewThumbTags:{},i=
nitialPreviewShowDelete:!0,removeFromPreviewOnError:!1,deleteUrl:"",delet=
eExtraData:{},overwriteInitial:!0,layoutTemplates:Y,previewTemplates:J,pr=
eviewZoomSettings:Q,previewZoomButtonIcons:{prev:'&lt;i =
class=3D"glyphicon glyphicon-triangle-left"&gt;&lt;/i&gt;',next:'&lt;i =
class=3D"glyphicon =
glyphicon-triangle-right"&gt;&lt;/i&gt;',toggleheader:'&lt;i =
class=3D"glyphicon =
glyphicon-resize-vertical"&gt;&lt;/i&gt;',fullscreen:'&lt;i =
class=3D"glyphicon =
glyphicon-fullscreen"&gt;&lt;/i&gt;',borderless:'&lt;i =
class=3D"glyphicon glyphicon-resize-full"&gt;&lt;/i&gt;',close:'&lt;i =
class=3D"glyphicon =
glyphicon-remove"&gt;&lt;/i&gt;'},previewZoomButtonClasses:{prev:"btn =
btn-navigate",next:"btn btn-navigate",toggleheader:"btn btn-default =
btn-header-toggle",fullscreen:"btn btn-default",borderless:"btn =
btn-default",close:"btn =
btn-default"},allowedPreviewTypes:null,allowedPreviewMimeTypes:null,allow=
edFileTypes:null,allowedFileExtensions:null,defaultPreviewContent:null,cu=
stomLayoutTags:{},customPreviewTags:{},previewSettings:ie,fileTypeSetting=
s:ae,previewFileIcon:'&lt;i class=3D"glyphicon =
glyphicon-file"&gt;&lt;/i&gt;',previewFileIconClass:"file-other-icon",pre=
viewFileIconSettings:{},previewFileExtSettings:{},buttonLabelClass:"hidde=
n-xs",browseIcon:'&lt;i class=3D"glyphicon =
glyphicon-folder-open"&gt;&lt;/i&gt;&amp;nbsp;',browseClass:"btn =
btn-primary",removeIcon:'&lt;i class=3D"glyphicon =
glyphicon-trash"&gt;&lt;/i&gt;',removeClass:"btn =
btn-default",cancelIcon:'&lt;i class=3D"glyphicon =
glyphicon-ban-circle"&gt;&lt;/i&gt;',cancelClass:"btn =
btn-default",uploadIcon:'&lt;i class=3D"glyphicon =
glyphicon-upload"&gt;&lt;/i&gt;',uploadClass:"btn =
btn-default",uploadUrl:null,uploadAsync:!0,uploadExtraData:{},zoomModalHe=
ight:480,minImageWidth:null,minImageHeight:null,maxImageWidth:null,maxIma=
geHeight:null,resizeImage:!1,resizePreference:"width",resizeQuality:.92,r=
esizeDefaultImageType:"image/jpeg",maxFileSize:0,maxFilePreviewSize:25600=
,minFileCount:0,maxFileCount:0,validateInitialCount:!1,msgValidationError=
Class:"text-danger",msgValidationErrorIcon:'&lt;i class=3D"glyphicon =
glyphicon-exclamation-sign"&gt;&lt;/i&gt; =
',msgErrorClass:"file-error-message",progressThumbClass:"progress-bar =
progress-bar-success progress-bar-striped =
active",progressClass:"progress-bar progress-bar-success =
progress-bar-striped active",progressCompleteClass:"progress-bar =
progress-bar-success",progressErrorClass:"progress-bar =
progress-bar-danger",previewFileType:"image",elCaptionContainer:null,elCa=
ptionText:null,elPreviewContainer:null,elPreviewImage:null,elPreviewStatu=
s:null,elErrorContainer:null,errorCloseButton:'&lt;span class=3D"close =
kv-error-close"&gt;&amp;times;&lt;/span&gt;',slugCallback:null,dropZoneEn=
abled:!0,dropZoneTitleClass:"file-drop-zone-title",fileActionSettings:{},=
otherActionButtons:"",textEncoding:"UTF-8",ajaxSettings:{},ajaxDeleteSett=
ings:{},showAjaxErrorDetails:!0},e.fn.fileinputLocales.en=3D{fileSingle:"=
file",filePlural:"files",browseLabel:"Browse =
&amp;hellip;",removeLabel:"Remove",removeTitle:"Clear selected =
files",cancelLabel:"Cancel",cancelTitle:"Abort ongoing =
upload",uploadLabel:"Upload",uploadTitle:"Upload selected =
files",msgNo:"No",msgCancelled:"Cancelled",msgZoomModalHeading:"Detailed =
Preview",msgSizeTooLarge:'File "{name}" (&lt;b&gt;{size} KB&lt;/b&gt;) =
exceeds maximum allowed upload size of &lt;b&gt;{maxSize} =
KB&lt;/b&gt;.',msgFilesTooLess:"You must select at least =
&lt;b&gt;{n}&lt;/b&gt; {files} to upload.",msgFilesTooMany:"Number of =
files selected for upload &lt;b&gt;({n})&lt;/b&gt; exceeds maximum =
allowed limit of &lt;b&gt;{m}&lt;/b&gt;.",msgFileNotFound:'File "{name}" =
not found!',msgFileSecured:'Security restrictions prevent reading the =
file "{name}".',msgFileNotReadable:'File "{name}" is not =
readable.',msgFilePreviewAborted:'File preview aborted for =
"{name}".',msgFilePreviewError:'An error occurred while reading the file =
"{name}".',msgInvalidFileType:'Invalid type for file "{name}". Only =
"{types}" files are supported.',msgInvalidFileExtension:'Invalid =
extension for file "{name}". Only "{extensions}" files are =
supported.',msgUploadAborted:"The file upload was =
aborted",msgValidationError:"Validation Error",msgLoading:"Loading file =
{index} of {files} &amp;hellip;",msgProgress:"Loading file {index} of =
{files} - {name} - {percent}% completed.",msgSelected:"{n} {files} =
selected",msgFoldersNotAllowed:"Drag &amp; drop files only! {n} =
folder(s) dropped were skipped.",msgImageWidthSmall:'Width of image file =
"{name}" must be at least {size} px.',msgImageHeightSmall:'Height of =
image file "{name}" must be at least {size} =
px.',msgImageWidthLarge:'Width of image file "{name}" cannot exceed =
{size} px.',msgImageHeightLarge:'Height of image file "{name}" cannot =
exceed {size} px.',msgImageResizeError:"Could not get the image =
dimensions to resize.",msgImageResizeException:"Error while resizing the =
image.&lt;pre&gt;{errors}&lt;/pre&gt;",dropZoneTitle:"Drag &amp; drop =
files here &amp;hellip;",dropZoneClickTitle:"&lt;br&gt;(or click to =
select {files})",previewZoomButtonTitles:{prev:"View previous =
file",next:"View next file",toggleheader:"Toggle =
header",fullscreen:"Toggle full screen",borderless:"Toggle borderless =
mode",close:"Close detailed =
preview"}},e.fn.fileinput.Constructor=3Dge,e(document).ready(function(){v=
ar =
t=3De("input.file[type=3Dfile]");t.length&amp;&amp;t.fileinput()})});</PR=
E></BODY></HTML>
