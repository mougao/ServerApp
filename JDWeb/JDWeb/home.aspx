<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="home.aspx.cs" Inherits="JDWeb.home" %>


<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head>
<meta http-equiv="Content-Type" content="text/html; charset=utf-8" />

<!-- Required Stylesheets -->
<link rel="stylesheet" type="text/css" href="css/reset.css" media="screen" />
<link rel="stylesheet" type="text/css" href="css/text.css" media="screen" />
<link rel="stylesheet" type="text/css" href="css/fonts/ptsans/stylesheet.css" media="screen" />
<link rel="stylesheet" type="text/css" href="css/fluid.css" media="screen" />

<link rel="stylesheet" type="text/css" href="css/mws.style.css" media="screen" />
<link rel="stylesheet" type="text/css" href="css/icons/icons.css" media="screen" />

<!-- Demo and Plugin Stylesheets -->
<link rel="stylesheet" type="text/css" href="css/demo.css" media="screen" />

<link rel="stylesheet" type="text/css" href="plugins/colorpicker/colorpicker.css" media="screen" />
<link rel="stylesheet" type="text/css" href="plugins/jimgareaselect/css/imgareaselect-default.css" media="screen" />
<link rel="stylesheet" type="text/css" href="plugins/fullcalendar/fullcalendar.css" media="screen" />
<link rel="stylesheet" type="text/css" href="plugins/fullcalendar/fullcalendar.print.css" media="print" />
<link rel="stylesheet" type="text/css" href="plugins/tipsy/tipsy.css" media="screen" />
<link rel="stylesheet" type="text/css" href="plugins/sourcerer/Sourcerer-1.2.css" media="screen" />
<link rel="stylesheet" type="text/css" href="plugins/jgrowl/jquery.jgrowl.css" media="screen" />
<link rel="stylesheet" type="text/css" href="plugins/spinner/spinner.css" media="screen" />
<link rel="stylesheet" type="text/css" href="css/jui/jquery.ui.css" media="screen" />

<!-- Theme Stylesheet -->
<link rel="stylesheet" type="text/css" href="css/mws.theme.css" media="screen" />

<link rel="stylesheet" type="text/css" href="css/jquery.dataTables.min.css" media="screen" />

<!-- JavaScript Plugins -->

<script src="js/jquery-3.3.1.min.js " type="text/javascript"> </script>

<script type="text/javascript" src="js/jquery.dataTables.min.js"></script>

    
<script src="js/mys.js " type="text/javascript"> </script>

<title>九道-测试后台</title>

</head>

<body>

	<div id="mws-themer">
    	<div id="mws-themer-hide"></div>
        <div id="mws-themer-content">
        	<div class="mws-themer-section">
	        	<label for="mws-theme-presets">Presets</label> <select id="mws-theme-presets"></select>
            </div>
            <div class="mws-themer-separator"></div>
            <div class="mws-themer-section">
                <ul>
                    <li><span>Base Color</span> <div id="mws-base-cp" class="mws-cp-trigger"></div></li>
                    <li><span>Text Color</span> <div id="mws-text-cp" class="mws-cp-trigger"></div></li>
                    <li><span>Text Glow Color</span> <div id="mws-textglow-cp" class="mws-cp-trigger"></div></li>
                </ul>
            </div>
            <div class="mws-themer-separator"></div>
            <div class="mws-themer-section">
            	<ul>
                    <li><span>Text Glow Opacity</span> <div id="mws-textglow-op"></div></li>
                </ul>
            </div>
            <div class="mws-themer-separator"></div>
            <div class="mws-themer-section">
	            <button class="mws-button red small" id="mws-themer-getcss">Get CSS</button>
            </div>
        </div>
        <div id="mws-themer-css-dialog">
        	<div class="mws-form">
            	<div class="mws-form-row" style="padding:0;">
		        	<div class="mws-form-item">
                    	<textarea cols="auto" rows="auto" readonly="readonly"></textarea>
                    </div>
                </div>
            </div>
        </div>
    </div>
    
	<div id="mws-header" class="clearfix">
    	
        
        <div id="mws-user-tools" class="clearfix">
        	
            <div id="mws-user-info" class="mws-inset">
            	<div id="mws-user-photo">
                	<img src="example/profile.jpg" alt="User Photo" />
                </div>
                <div id="mws-user-functions">
                    <div id="mws-username">
                        Hello, guys
                    </div>
                    <ul>
                        <li><a href="index.aspx">登出</a></li>
                    </ul>
                </div>
            </div>
        </div>
    </div>
    
    <div id="mws-wrapper">
		<div id="mws-sidebar-stitch"></div>
		<div id="mws-sidebar-bg"></div>
        <div id="mws-sidebar">
            <div id="mws-navigation">
            	<ul>
                	<li><a href="home.aspx" class="mws-i-24 i-home">麻将设置</a></li>
                	<li><a href="setpoker.aspx" class="mws-i-24 i-chart">扑克设置</a></li>

                </ul>
            </div>
        </div>
        
        <div id="mws-container" class="clearfix">
            <div class="container">
            
            	<div class="mws-panel grid_8">
                	<div class="mws-panel-header">
                    	<span class="mws-i-24 i-list">麻将牌配置</span>
                        
                    </div>
                    <div class="mws-panel-body">
                    	<div class="mws-form">
                    		<div class="mws-form-inline">
                                <div class="mws-form-row">

                    				<label>游戏选择</label>
                    				<div class="mws-form-item small">
                    					<select id ="gametype">
         
                    					</select>
                    				</div>
                    			</div>
                    	
                                <div class="mws-form-row">
                    				<label>设置牌型</label>
                    				<div class="mws-form-item large">
                                    	<div class="mws-dualbox clearfix">
                                        	<div style="float:left">
                                                <span id="box1Counter" class="countLabel">配置牌堆 0 </span>
                                                <div style="display:block;width:135px">
                                                    <input id="Button1" type="button" value=">>" />
                                                    <input id="btnclear" type="button" value="<<" />
                                                </div>
                                                
                                                <select id="box1View" multiple="multiple" style="height:270px;width:100px">

                                                </select>
                                            </div>
  
                                            <div style="float:left">
	                                            <span id="box2Counter" class="countLabel">庄家 0 </span>
                                                <div style="display:block">
                                                    <input id="Button2" type="button" value="<<" />
                                                </div>
                                                <select id="box2View" multiple="multiple" style="height:270px;width:100px"></select>
                                                
                                            </div>

                                            <div style="float:left">

                                                <span id="box3Counter" class="countLabel">玩家一 0 </span>
                                                <div style="display:block">
                                                    <input id="Button3" type="button" value="<<" />
                                                </div>
                                                <select id="box3View" multiple="multiple" style="height:270px;width:100px"></select>

                                            </div>
                                            <div style="float:left">

                                                <span id="box4Counter" class="countLabel">玩家二 0 </span>
                                                <div style="display:block">
                                                    <input id="Button4" type="button" value="<<" />
                                                </div>
                                                <select id="box4View" multiple="multiple" style="height:270px;width:100px"></select>

                                            </div>

                                            <div style="float:left">

                                                <span id="box5Counter" class="countLabel">玩家三 0 </span>
                                                <div style="display:block">
                                                    <input id="Button5" type="button" value="<<" />
                                                </div>
                                                <select id="box5View" multiple="multiple" style="height:270px;width:100px"></select>

                                            </div>

                                            <div style="float:left">

                                                <span id="box6Counter" class="countLabel">牌堆 0 </span>
                                                <div style="display:block">
                                                    <input id="Button6" type="button" value="<<" />
                                                </div>
                                                <select id="box6View" multiple="multiple" style="height:270px;width:100px"></select>

                                            </div>


                                        </div>
                                    </div>
                    			</div>

                                <div class="mws-form-row">
                    				<label>设置牌型备注</label>
                    				<div class="mws-form-item large">
                    					<input id="remarks" type="text" class="mws-textinput" />
                    				</div>
                    			</div>

                    			<div class="mws-form-row">
                    				<label>立即激活</label>
                    				<div class="mws-form-item clearfix">
                    					<ul class="mws-form-list inline">
                    						<li><input id="isuse" type="checkbox" /> <label>激活</label></li>
                    					</ul>
                    				</div>
                    			</div>
                    			
                    		</div>
                    		<div class="mws-button-row">
                    			<input id="button1" type="submit" value="提交" class="mws-button black" />
    
                    		</div>
                    	</div>
                    </div>    	
                </div>


                <div class="mws-panel grid_8">
                	<div class="mws-panel-header">
                    	<span class="mws-i-24 i-table-1">当前牌型</span>
                    </div>
                    <div class="mws-panel-body" style=" overflow:auto;">
                        <table id="table1" class="mws-datatable-fn mws-table">
                          <thead>
                                <tr>
                                    <th >序号</th>
                                    <th >游戏类型</th>
                                    <th >牌型信息</th>
                                    <th >牌型备注</th>
                                    <th >激活状态</th>
                                    <th >操作</th>
                                </tr>
                            </thead>
 
                            
                        </table>
                    </div>
                </div>

            </div>
            
            <div id="mws-footer">
            	宁波九道网络科技有限公司
            </div>
            
        </div>
    </div>


</body>
</html>

