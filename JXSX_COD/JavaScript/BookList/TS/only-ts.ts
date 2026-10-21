
// 设定参数类型
/* function greet(person: string, date: Date) {
  console.log(`Hello ${person}, today is ${date.toDateString()}!`);
}
 
greet("Maddison", new Date()); 
*/

// 函数返回值类型
/*
function getFavoriteNumber(): number {
  return 26;
}
const a = getFavoriteNumber();
console.log(a);
*/

//类型推断
/*
const names = ["Alice", "Bob", "Eve"];
//函数的上下文类型推断
names.forEach(function (s) {
  console.log(s.toUpperCase());
});
 names.forEach(s=> {
  console.log(s.toUpperCase());
});
*/


// function printCoord(pt: { x: number; y: number }) {
//   console.log("坐标的 x 值是 " + pt.x);
//   console.log("坐标的 y 值是 " + pt.y);
// }
// printCoord({ x: 3, y: 7 });

//?可选参数
/*
function printName(obj: { first: string; last?: string }) {
 if(obj.last ===undefined)
  console.log(obj.first);
  else
  console.log(obj.first + obj.last);
}
printName({ first: "Bob" });
printName({ first: "Alice", last: "Alisson" });
*/


//联合类型, 类型可选
/*
function printId(id: number | string) {
  if (typeof id === "string") {
    // 在这个分支中，id 的类型是 'string'
    console.log(id.toUpperCase());
  } else {
    // 在这里，id 的类型是 'number'
    console.log(id);
  }
}
printId("sajfe");
*/
/*function welcomePeople(x: string[] | string) {
  if (Array.isArray(x)) {
    // 在这里：'x' 的类型是 'string[]'
    console.log("你好，" + x.join(" 和 "));
  } else {
    // 在这里：'x' 的类型是 'string'
    console.log("欢迎，孤独旅行者 " + x);
  }
}
let x = ["a","b","c"];
let y = "ssss";
welcomePeople(y);*/


//type类型别名
/*
type str = string;
let a:str="aaaaa";
console.log(a);
*/
/*
type Point = {
  x: number;
  y: number;
};
// 与前面的示例完全相同
function printCoord(pt: Point) {
  console.log("x 的坐标值是 " + pt.x);
  console.log("y 的坐标值是 " + pt.y);
}
printCoord({ x: 100, y: 100 });
*/



/* interface Point {
  x: number;
  y: number;
}
function printCoord(pt: Point) {
  console.log("x 的坐标值是 " + pt.x);
  console.log("y 的坐标值是 " + pt.y);
  console.log(pt.add(pt.x,pt.y));
}
printCoord({ x: 100, y: 100 }); */



