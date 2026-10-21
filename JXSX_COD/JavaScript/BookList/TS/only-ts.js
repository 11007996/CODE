"use strict";
// 设定参数类型
/* function greet(person: string, date: Date) {
  console.log(`Hello ${person}, today is ${date.toDateString()}!`);
}
 
greet("Maddison", new Date());
*/
function add(x, y) {
    console.log(x + y);
}
function printCoord(pt) {
    console.log("x 的坐标值是 " + pt.x);
    console.log("y 的坐标值是 " + pt.y);
    console.log(pt.add(pt.x, pt.y));
}
printCoord({ x: 100, y: 100 });
