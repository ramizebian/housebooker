<%@ Page Title="" Language="VB" MasterPageFile="~/MasterPage.master" AutoEventWireup="false" CodeFile="notifications.aspx.vb" Inherits="notifications" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <section>
        <div class="container">
            <div class="row">
                <div class="col-12">
                    <a href="dashboard">Back to Dashboard</a>
                </div>
            </div>    
            <div class="row">
                <div class="col-lg-9 col-md-12 col-sm-12 col-12">
                    <div class="box">
                        <h6>Rami, you have 1 notification:</h6>
                        <ol>
                            <li>You usually turn off your AC/Heater at 8:00 am everyday. You might have missed that today. If you would like to turn it off please click <a href="details">here</a></li>
                        </ol>
                    </div>
                </div>
            </div>
        </div>
    </section>
</asp:Content>

