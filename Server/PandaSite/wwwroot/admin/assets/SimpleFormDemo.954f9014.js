var V=Object.defineProperty;var k=(t,e,l)=>e in t?V(t,e,{enumerable:!0,configurable:!0,writable:!0,value:l}):t[e]=l;var a=(t,e,l)=>(k(t,typeof e!="symbol"?e+"":e,l),l);import{d as f,l as F,r as D,a as p,e as n,j as u,a0 as I,S as g,F as O,o as S,t as b,f as d,D as h,x as w,h as v}from"./index.42ecf877.js";class _{constructor(){a(this,"control","input")}}class y extends _{constructor(){super(...arguments);a(this,"control","input")}}class T extends _{constructor(){super(...arguments);a(this,"control","select");a(this,"items")}}const j=f({props:{items:{type:Object,required:!0}},setup(t){const e=()=>{console.log("\u70B9\u51FB\u4E00\u4E0B")};F(()=>{console.log("\u7EC4\u4EF6\u542F\u52A8")}),D({x:0,y:0});const l=p("123");return()=>{var s;return n(O,null,[(s=t.items)==null?void 0:s.map(o=>{var r;if(o.control=="input")return n("h1",null,[u("input")]);if(o.control=="select"){var m=o;return n("div",null,[(r=m.items)==null?void 0:r.map(i=>n("p",null,[u("\u914D\u7F6E:"),i.label,u(" - "),i.value]))])}return null}),n(I,{modelValue:l.value,"onUpdate:modelValue":o=>l.value=o},null),n(g,{onClick:e},{default:()=>[u("\u6D4B\u8BD5-click")]})])}}}),H=j,$=f({__name:"SimpleFormDemo",setup(t){const e=p(!1),l=p(""),s=p("h"),o=new T;o.items=[{label:"\u6D4B\u8BD5",value:"\u6D4B\u8BD5\u7684\u503C"}];const m=D([new y,o]),r=()=>{s.value=s.value=="v"?"h":"v",console.log("\u66F4\u65B0\u4E86")};return(i,c)=>{const E=v("ElButton"),x=v("ElInput"),C=v("ElCard");return S(),b(C,null,{header:d(()=>[u(" \u6D4B\u8BD5\u8868\u5355:"+h(e.value),1)]),default:d(()=>[n(E,{onClick:c[0]||(c[0]=B=>r())},{default:d(()=>[u("test")]),_:1}),n(w(H),{items:m},null,8,["items"]),n(x,{modelValue:l.value,"onUpdate:modelValue":c[1]||(c[1]=B=>l.value=B)},null,8,["modelValue"])]),_:1})}}});export{$ as default};