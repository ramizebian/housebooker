<%@ Page Title="" Language="VB" MasterPageFile="~/MasterPage.master" AutoEventWireup="false" CodeFile="index.aspx.vb" Inherits="index" %>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
<script src="https://www.google.com/recaptcha/api.js" async defer></script>
<%--<form runat="server">
    <asp:ScriptManager runat="server"></asp:ScriptManager>
</form>--%>

<section>
    <div class="container">
        <div class="row">
            <div class="col-4"></div>
            <div class="col-lg-4 col-md-6 col-sm-12 col-12 text-center">
                <img alt="" src="images/logo.png" class="img-fluid" />
                <div class="box box-landing mt-3">
                    <div class="fb-login-button" data-max-rows="1" data-size="large" data-button-type="login_with" data-show-faces="false" data-auto-logout-link="true" data-use-continue-as="true" scope="public_profile,email,user_friends"
                         onlogin="checkLoginState();"></div>
                </div>
            </div>
            <div class="col-4"></div>
        </div>
    </div>
</section>
</asp:Content>