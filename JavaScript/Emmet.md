### 基本语法

- 后代：>

   ```HTML
   语法：nav>ul>li
    
   <nav>
       <ul>
           <li></li>
       </ul>
   </nav>
   ```

- 兄弟：+

  ```HTML
  语法：div+p+bq
      
  <div></div>
  <p></p>
  <blockquote></blockquote>
  ```

- 上级：^

  ```HTML
  语法：div+div>p>span+em^bq
      
  <div></div>
  <div>
      <p>
        <span></span>
        <em></em>
      </p>
      <blockquote></blockquote>
  </div>
  ```

- 分组：()

  ```HTML
  语法：div>(header>ul>li*2>a)+footer>p
      
  <div>
      <header>
          <ul>
              <li><a href=""></a><li>
              <li><a href=""></a><li>
          </ul>
      </header>
      </footer>
          <p></p>
      </footer>
  </div>
  ```

- 乘法：*
  
  ```html
  语法：ul>li*5
    
  <ul>
      <li></li>
      <li></li>
      <li></li>
      <li></li>
      <li></li>
  <ul>
  ```
  
- 自增符号：$
  
  ```html
  语法：ul>li.item$*5
    
  <ul>
      <li class="item1"></li>
      <li class="item2"></li>
      <li class="item3"></li>
      <li class="item4"></li>
      <li class="item5"></li>
  <ul>  
  ```
  
- ID属性：#

  ```html
  语法：div#na
  
  <div id="na"></div>
  ```

- 类属性：.

   ```html
   语法：div.na
   
   <div class="na"></div>
   ```

- 自定义属性

  ```html
  语法：p[title="Hello world"]
  
  <p title="Hello world"></p>
  ```

- 文本：{}

  ```html
  语法：a{click me}
  
  <a href="">click me</a>
  ```

- 缩写

  ```html
  语法：a:link
  <a href="http://"></a>
  
  语法：a:mail
  <a href="mailto:"></a>
  
  语法：link
  <link rel="stylesheet" href="">
  
  语法：link:css
  <link rel="stylesheet" href="style.css">
  
  语法：link:print
  <link rel="stylesheet" href="print.css" media="print" />
  
  语法：link:favicon
  <link rel="shortcut icon" type="image/x-icon" href="favicon.ico" />
  
  语法：link:link:touch
  <link rel="apple-touch-icon" href="favicon.png" />
  
  语法：link:rss
  <link rel="alternate" type="application/rss+xml" title="RSS" href="rss.xml" />
  
  语法：meta:utf
  <meta http-equiv="Content-Type" content="text/html;charset=UTF-8" />
  
  语法：meta:win
  <meta http-equiv="Content-Type" content="text/html;charset=windows-1251" />
  
  语法：meta:meta:vp
  <meta name="viewport" content="width=device-width, user-scalable=no, initial-scale=1.0, maximum-scale=1.0, minimum-scale=1.0" />
  
  语法：meta:compat
  <meta http-equiv="X-UA-Compatible" content="IE=7" />
  
  语法：script:src
  <script src=""></script>
  
  语法：img
  <img src="" alt="" />
  
  语法：iframe
  <iframe src="" frameborder="0"></iframe>
  
  语法：embed
  <embed src="" type="" />
  
  语法：area
  <area shape="" coords="" href="" alt="" />
  
  语法：area:d
  <area shape="default" href="" alt="" />
  
  语法：area:c
  <area shape="circle" coords="" href="" alt="" />
  
  语法：area:r
  <area shape="rect" coords="" href="" alt="" />
  
  语法：form
  <form action=""></form>
  
  语法：form:post
  <form action="" method="post"></form>
  
  语法：form:get
  <form action="" method="get"></form>
  
  语法：inp
  <input type="text" name="" id="" />
  
  语法：input:h 
  <input type="hidden" name="" />
  
  语法：input:search
  <input type="search" name="" id="" />
  
  语法：input:email 
  <input type="email" name="" id="" />
  
  语法：html:xml
  <html xmlns="http://www.w3.org/1999/xhtml"></html>
  
  语法：ifr
  <iframe src="" frameborder="0"></iframe>
  ```

- CSS缩写

  ```css
  语法：pos
  position:relative;
  
  语法：t
  top:;
  ```
