<%@ Page Title="" Language="VB" MasterPageFile="~/MasterPage.master" AutoEventWireup="false" CodeFile="log.aspx.vb" Inherits="log" %>

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
                        <h6>Your log details:</h6>
                        <ol>
                            <li>01/05/2018 - Turned off the lights at home</li>
                            <li>02/05/2018 - Turned off the lights at Ben's house.</li>
                        </ol>
                    </div>
                </div>
            </div>
        </div>
    </section>
</asp:Content>

