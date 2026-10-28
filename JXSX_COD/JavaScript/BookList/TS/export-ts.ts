//此命名空间可以导出用于其它模块，声明前都在加上export
export namespace exportTs
{
  export function show()
  {
    console.log("exportTs function show");
  }
}



export namespace decal
{
  export class CC{
    show(name:string):void
    {
      console.log(name);
    }
  }
}
