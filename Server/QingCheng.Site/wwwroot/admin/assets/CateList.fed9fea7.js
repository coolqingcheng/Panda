import{d as w,a as x,r as C,s as h,l as B,o as k,t as V,f as t,e as a,x as e,S as d,B as y,y as N,T as u,j as _,C as E,U as T,V as S,b as R,R as L,h as r,m as P}from"./index.42ecf877.js";import{t as U,p as $}from"./ElConfig.4a51009f.js";const D=R("div",{class:"text-title"}," \u5206\u7C7B ",-1),A=w({__name:"CateList",setup(M){const f=P(),m=l=>{l!=null?f.push({name:"\u7F16\u8F91\u5206\u7C7B",query:{id:l.id,cateName:l.cateName}}):f.push({name:"\u7F16\u8F91\u5206\u7C7B"})};var p=x(!1);const o=C({index:1,pageSize:10,total:0});h(()=>o.index,()=>{c()});var i=C([]);const c=()=>{p.value=!0,L.getList({...o}).then(l=>{i=[],l.data&&(i=l.data),console.log(i),o.total=l.total}).finally(()=>p.value=!1)};return B(()=>{c()}),(l,n)=>{const F=r("ElSpace"),g=r("ElCol"),v=r("ElRow"),b=r("ElPagination");return k(),V(e(S),null,{header:t(()=>[D]),default:t(()=>[a(v,null,{default:t(()=>[a(g,null,{default:t(()=>[a(F,null,{default:t(()=>[a(e(d),{icon:e(y),onClick:n[0]||(n[0]=s=>m(null))},null,8,["icon"]),a(e(d),{icon:e(N),onClick:n[1]||(n[1]=s=>c())},null,8,["icon"])]),_:1})]),_:1})]),_:1}),a(e(T),E({data:e(i),loading:e(p)},e(U)),{default:t(()=>[a(e(u),{label:"Id",width:"80",prop:"id"}),a(e(u),{label:"\u540D\u79F0",prop:"cateName"}),a(e(u),{label:"\u5173\u8054\u6570\u91CF",width:"120",prop:"postCount"}),a(e(u),{label:"\u521B\u5EFA\u65F6\u95F4",width:"180",prop:"createTime"}),a(e(u),{label:"\u6700\u540E\u4FEE\u6539\u65F6\u95F4",width:"180",prop:"lastUpdateTime"}),a(e(u),{label:"\u64CD\u4F5C",width:"120"},{default:t(s=>[a(e(d),{type:"primary",link:"",onClick:j=>m(s.row)},{default:t(()=>[_("\u7F16\u8F91")]),_:2},1032,["onClick"]),a(e(d),{type:"danger",link:""},{default:t(()=>[_("\u5220\u9664")]),_:1})]),_:1})]),_:1},16,["data","loading"]),a(b,E(e($),{total:o.total,modelValue:o.index,"onUpdate:modelValue":n[2]||(n[2]=s=>o.index=s)}),null,16,["total","modelValue"])]),_:1})}}});export{A as default};
