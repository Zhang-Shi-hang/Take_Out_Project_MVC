using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Take_Out_Project_MVC.Models
{
    public class ViewModel
    {
        #region 评价表
        /// <summary>
        /// 评价表
        /// </summary>
        public Guid CommentId { get; set; }//评价主键Id
        public string CommentContent { get; set; }//评价内容
        public DateTime CommentTime { get; set; }//评价时间
        public int CommentScore { get; set; }//评价分数
        #endregion

        #region 明细表

        /// <summary>
        /// 明细表
        /// </summary>
        public Guid DetailId { get; set; }//明细主键Id
        public int Gnum { get; set; }//菜品数量
        public int Gsum { get; set; }//菜品总价
        public decimal Gprice { get; set; }//菜品价格   --可用可不用
        #endregion

        #region 菜品表
        /// <summary>
        /// 菜品表
        /// </summary>
        public Guid GreensId { get; set; }//菜品主键Id
        public string GreensName { get; set; }//菜品姓名
        public decimal GreensPrice { get; set; }//菜品价格
        public int GreensNum { get; set; }//菜品数量
        public string GreensRemark { get; set; }//菜品备注
        public int GreensLikeNum { get; set; }//喜欢人数
        public string GreensType { get; set; }//菜品类型
        public string GreensImgUrl { get; set; }//菜品图片
        #endregion

        #region 骑士表
        /// <summary>
        /// 骑士表
        /// </summary>
        public Guid HorsemanId { get; set; }//骑士主键Id
        public string HorsemanName { get; set; }//骑士姓名
        public string HorsemanPhone { get; set; }//骑士手机号
        #endregion

        #region 管理表
        /// <summary>
        /// 管理表
        /// </summary>
        public Guid ManageId { get; set; }//管理主键Id
        public string ManageAccount { get; set; }//管理账号
        public string ManagePwd { get; set; }//管理密码
        #endregion

        #region 订单表
        /// <summary>
        /// 订单表
        /// </summary>
        public Guid OrderId { get; set; }//订单主键Id
        public int Oen { get; set; }//订单编号
        public int OrderStatic { get; set; }//订单状态
        public DateTime OrderTime { get; set; }//订单时间
        public string OrderRemark { get; set; }//订单备注
        public decimal OrderPrice { get; set; }//订单价格
        public bool RepastWay { get; set; }//就餐方式
        public decimal OrderSum { get; set; }//订单总价
        #endregion

        #region 店铺表
        /// <summary>
        /// 店铺表
        /// </summary>
        public Guid ShopId { get; set; }//店铺主键Id
        public string ShopName { get; set; }//店铺姓名
        public string ShopPhone { get; set; }//店铺电话
        public string ShopImgUrl { get; set; }//店铺图片
        public string ShopAddress { get; set; }//店铺地址
        public int ShopLikeNum { get; set; }//喜欢店铺人数
        public int ShopMonthSell { get; set; }//店铺月售
        public string ShopRemark { get; set; }//店铺备注
        public int Shopcapita { get; set; }//店铺人均
        public DateTime ShopCreateTime { get; set; }//店铺创建时间
        public int ShopUpPrice { get; set; }//店铺起送价
        public int ShopStartTime { get; set; }//店铺开门时间
        public int ShopEndTime { get; set; }//店铺关门时间
        #endregion

        #region 用户表
        /// <summary>
        /// 用户表
        /// </summary>
        public Guid UserId { get; set; }//用户主键Id
        public string UserPhone { get; set; }//用户手机号
        public string UserName { get; set; }//用户姓名
        public bool UserSex { get; set; }//用户性别
        public string UserAddress { get; set; }//用户地址
        public string UserImgUrl { get; set; }//用户图片
        public int UserIntegral { get; set; }//用户积分
        public int UserStatic { get; set; }//用户状态

        #endregion

        #region 退单表
        /// <summary>
        /// 退单表
        /// </summary>
        public Guid RefundId { get; set; }
        public string RefundCause { get; set; }
        public string RefundExplain { get; set; }
        #endregion

        #region 优惠表
        /// <summary>
        /// 优惠表
        /// </summary>
        public Guid DiscountsId { get; set; }//优惠主键
        public int FavourablePrice { get; set; }//优惠价格
        public int SustainDay { get; set; }//优惠券有效天数
        public int DiscountStatic { get; set; }//优惠券状态
        #endregion
        //外键
        public Guid Uid { get; set; }//用户外键          评价表 优惠表 订单表 
        public Guid Sid { get; set; }//店铺外键          评价表 优惠表 菜品表
        public Guid Hid { get; set; }//骑士外键          订单表 
        public Guid Gid { get; set; }//菜品外键          明细表 
        public Guid Oid { get; set; }//订单外键          退款表 明细表
    }
}