import{d as v,l as B,r as N,a as k,o as D,t as g,f as o,b as x,e as a,x as i,Q as y,j as l,g as V,m as S,q as I,R as M,E as R,h as n}from"./index.42ecf877.js";const q={class:"text-large"},$=v({__name:"CateEdit",setup(w){const s=S(),p=I();B(()=>{var e;var t=p.query;t&&(console.log(t),t.id&&(u.id=Number(t.id)),u.cateName=(e=t.cateName)==null?void 0:e.toString())});const u=N({cateName:"",id:0}),f=()=>{s.go(-1)},d=k(),_=()=>{var t;(t=d.value)==null||t.validate(e=>{e&&M.edit({body:u}).then(()=>{R({message:"\u4FDD\u5B58\u6210\u529F",type:"success"}),s.replace({name:"\u6587\u7AE0\u5206\u7C7B"})})})};return(t,e)=>{const r=n("ElButton"),E=n("ElSpace"),C=n("ElInput"),c=n("ElFormItem"),F=n("ElForm"),b=n("ElCard");return D(),g(b,null,{header:o(()=>[x("span",q,[a(E,null,{default:o(()=>[a(r,{text:"",icon:i(y),onClick:e[0]||(e[0]=()=>i(s).back())},{default:o(()=>[l("\u8FD4\u56DE")]),_:1},8,["icon"]),l(" \u7F16\u8F91\u5206\u7C7B ")]),_:1})])]),default:o(()=>[a(F,{"label-position":"top",model:u,ref_key:"form",ref:d,onSubmit:e[4]||(e[4]=V(()=>{},["prevent"]))},{default:o(()=>[a(c,{label:"\u5206\u7C7B\u540D\u79F0",prop:"cateName"},{default:o(()=>[a(C,{placeholder:"\u8F93\u5165\u5206\u7C7B\u540D\u79F0",modelValue:u.cateName,"onUpdate:modelValue":e[1]||(e[1]=m=>u.cateName=m)},null,8,["modelValue"])]),_:1}),a(c,{label:""},{default:o(()=>[a(r,{type:"primary","native-type":"submit",onClick:e[2]||(e[2]=m=>_())},{default:o(()=>[l("\u786E\u5B9A")]),_:1}),a(r,{onClick:e[3]||(e[3]=m=>f())},{default:o(()=>[l("\u8FD4\u56DE")]),_:1})]),_:1})]),_:1},8,["model"])]),_:1})}}});export{$ as default};
