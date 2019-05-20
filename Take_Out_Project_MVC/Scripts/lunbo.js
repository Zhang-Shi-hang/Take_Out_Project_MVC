window.onload=function(){
	var odiv=document.getElementById("odiv");
	var left=document.getElementById("left");
	var ul1=document.getElementById("ul1");
	var img=ul1.getElementsByTagName("img");
	var right=document.getElementById("right");
	var ul2=document.getElementById("ul2");
	var lis=ul2.getElementsByTagName("li");
	var time=null;
	var start=0;
	time=setInterval(fun,1000);
	function fun()
	{
		start++;
		for(var i=0;i<img.length;i++)
		{
			img[i].style.display="none";
			lis[i].className="";
		}
		if(start>=img.length)
		{
			start=0;
		}
		img[start].style.display="block";
		lis[start].className="a";
		odiv.onmouseover=function()
		{
			clearInterval(time);
			left.style.display="block";
			right.style.display="block";
			time=null;
		}
		odiv.onmouseout=function()
		{
			time=setInterval(fun,1000);
			left.style.display="none";
			right.style.display="none";
		}
		for(var i=0;i<lis.length;i++)
		{
			lis[i].index=i;
			lis[i].onmouseover=function()
			{
				for(var i=0;i<img.length;i++)
				{
					img[i].style.display="none";
					lis[i].className="";
				}
				img[this.index].style.display="block";
				lis[this.index].className="a";
				start=this.index;
			}

		}

		right.onclick=function()
		{
				fun();
		}
		left.onclick=function()
		{
			start--;
			for(var i=0;i<img.length;i++)
			{
				img[i].style.display="none";
				lis[i].className="";
			}
			if(start<0)
			{
				start=img.length-1;
			}
			img[start].style.display="block";
			lis[start].className="a";
		}

	}

}