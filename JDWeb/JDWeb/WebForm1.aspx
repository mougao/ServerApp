<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="WebForm1.aspx.cs" Inherits="JDWeb.WebForm1" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title></title>
    <script src="js/jquery-3.3.1.min.js " type="text/javascript"> </script>
</head>
<body>
    <script type="text/javascript">

     $(function(){

                   var self = this;

                   this.curData = null;

                   this.$sorc = $('#allMember');

                   $("#allMember").on("mousedown","option",function(e){

                   self.curData = e.target;

                  });	

                  $(document).on("mouseup",function(e){

                    var tmpDom = e.target.id;

                    if("teamLeader" != tmpDom && "teamMem" != tmpDom){//在放置区以外的地方就清空数据

                    var $dom = $(self.curData);

                    $dom.css("position","static");

                    self.curData = null;

                    return ;

                   }

       

                   if(null != self.curData){

                     var $dom = $(self.curData);

                     $dom.css("position","static");

                     $("#"+tmpDom).append($dom);

                     $dom.click(function(){

                     $(this).appendTo(self.$sorc);

                    });

                   }

                     self.curData = null;

                  });

      

                  //元素跟随移动效果

                  $(document).on("mousemove",function(e){

                    if(null != self.curData){

                     var $dom = $(self.curData);

                     $dom.css({position: "absolute",'top':e.pageY+10,'left':e.pageX+10,'z-index':2});   

                   }

                  });

     });

 </script>


    <form id="form1" runat="server">
        <div style="margin-top:150px;margin-left:150px">

              所有成员

              <select id="allMember" multiple="" size="auto" class="form-control select"

               style="width:410px;font-size:12px;display:inline-block;">

                 <option value ="volvo">Volvo</option>

                 <option value ="saab">Saab</option>

                 <option value="opel">Opel</option>

                 <option value="audi">Audi</option>

              </select>

  

              <div style="display:inline-block; vertical-align:top;">

               队长

               <select id="teamLeader" multiple="" size="3" class="form-control select" 

                style="width:410px;font-size:12px;margin-bottom:20px;">

               </select>

               队员

               <select id="teamMem" multiple="" size="auto" class="form-control select" 

                style="width:410px;font-size:12px;">

               </select>

              </div>

  

              <button id="subTeam">提交</button>

             </div>
    </form>
</body>
</html>
