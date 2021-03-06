﻿<%@ Page Title="" Language="VB" MasterPageFile="~/MasterPage.master" AutoEventWireup="false" CodeFile="dashboard.aspx.vb" Inherits="dashboard" %>
<%@ Register Src="~/controls/authentication.ascx" TagPrefix="uc1" TagName="authentication" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <uc1:authentication runat="server" ID="authentication" />
    <section>
        <div class="container">
            <div class="row">
                <div class="col-lg-2 col-md-12 col-sm-12 col-12">
                    <div class="box">
                        <div class="text-center">
                            <div><img id="imgAvatar" alt="" class="rounded-circle" /></div>
                            <label class="name my-2" id="lblName"></label>
                            
                            <div class="profile-summary">
                                My Devices: 14<br/>
                                Other Devices: 3<br/>
                                Actions last 24 hours: 3 <a href="log">(View log)</a><br/><br/>
                                <a href="notifications" class="alert-danger p-2 mt-2">Notifications: 1</a>
                            </div>
                            <div><a href="javascript:facebooklogout();" class="nav-link">Logout</a></div>
                        </div>
                    </div>
                </div>
                <div class="col-lg-7 col-md-12 col-sm-12 col-12">
                    <div class="row">
                        <div class="col-12">
                            <div class="box">
                                <h6>My Devices</h6><br/>
                                
                                <h6 class="subtitle">My House</h6>
                                <div class="row">
                                    <div class="col-lg-4 col-md-4 col-sm-12 col-12 mb-3">
                                        <a href="details">
                                            <div class="box-iot">
                                                Curtains
                                            </div>
                                        </a>
                                    </div>
                                    <div class="col-lg-4 col-md-4 col-sm-12 col-12 mb-3">
                                        <a href="details">
                                            <div class="box-iot">
                                                Microwave
                                            </div>
                                        </a>
                                    </div>
                                    <div class="col-lg-4 col-md-4 col-sm-12 col-12 mb-3">
                                        <a href="details">
                                            <div class="box-iot">
                                                Door Lock
                                            </div>
                                        </a>
                                    </div>
                                    <div class="col-lg-4 col-md-4 col-sm-12 col-12 mb-3">
                                        <a href="details">
                                            <div class="box-iot">
                                                AC / Heater
                                            </div>
                                        </a>
                                    </div>
                                    <div class="col-lg-4 col-md-4 col-sm-12 col-12 mb-3">
                                        <a href="details">
                                            <div class="box-iot">
                                                Car
                                            </div>
                                        </a>
                                    </div>
                                    <div class="col-lg-4 col-md-4 col-sm-12 col-12 mb-3">
                                        <a href="details">
                                            <div class="box-iot">
                                                Lights
                                            </div>
                                        </a>
                                    </div>
                                    <div class="col-lg-4 col-md-4 col-sm-12 col-12 mb-3">
                                        <a href="add">
                                            <div class="box-iot">
                                                <i class="fas fa-plus"></i>
                                            </div>
                                        </a>
                                    </div>
                                </div>

                                <hr/>
                                
                                <h6 class="subtitle">My Workplace</h6>
                                <div class="row">
                                    <div class="col-lg-4 col-md-4 col-sm-12 col-12 mb-3">
                                        <a href="details">
                                            <div class="box-iot">
                                                Curtains
                                            </div>
                                        </a>
                                    </div>
                                    <div class="col-lg-4 col-md-4 col-sm-12 col-12 mb-3">
                                        <a href="details">
                                            <div class="box-iot">
                                                Microwave
                                            </div>
                                        </a>
                                    </div>
                                    <div class="col-lg-4 col-md-4 col-sm-12 col-12 mb-3">
                                        <a href="details">
                                            <div class="box-iot">
                                                Door Lock
                                            </div>
                                        </a>
                                    </div>
                                    <div class="col-lg-4 col-md-4 col-sm-12 col-12 mb-3">
                                        <a href="details">
                                            <div class="box-iot">
                                                AC / Heater
                                            </div>
                                        </a>
                                    </div>
                                    <div class="col-lg-4 col-md-4 col-sm-12 col-12 mb-3">
                                        <a href="details">
                                            <div class="box-iot">
                                                Lights
                                            </div>
                                        </a>
                                    </div>
                                    <div class="col-lg-4 col-md-4 col-sm-12 col-12 mb-3">
                                        <a href="add">
                                            <div class="box-iot">
                                                <i class="fas fa-plus"></i>
                                            </div>
                                        </a>
                                    </div>
                                </div>
                                
                                <hr/>
                                
                                <h6 class="subtitle">My Music Studio</h6>
                                <div class="row">
                                    <div class="col-lg-4 col-md-4 col-sm-12 col-12 mb-3">
                                        <a href="details">
                                            <div class="box-iot">
                                                Curtains
                                            </div>
                                        </a>
                                    </div>
                                    <div class="col-lg-4 col-md-4 col-sm-12 col-12 mb-3">
                                        <a href="details">
                                            <div class="box-iot">
                                                AC / Heater
                                            </div>
                                        </a>
                                    </div>
                                    <div class="col-lg-4 col-md-4 col-sm-12 col-12 mb-3">
                                        <a href="details">
                                            <div class="box-iot">
                                                Lights
                                            </div>
                                        </a>
                                    </div>
                                    <div class="col-lg-4 col-md-4 col-sm-12 col-12 mb-3">
                                        <a href="add">
                                            <div class="box-iot">
                                                <i class="fas fa-plus"></i>
                                            </div>
                                        </a>
                                    </div>
                                </div>
                                
                                <hr/>
                                <div class="row">
                                    <div class="col-lg-4 col-md-4 col-sm-12 col-12 mb-3">
                                        <a href="#">
                                            <div class="box-iot inverted">
                                                <i class="fas fa-plus"></i> Add New Place
                                            </div>
                                        </a>
                                    </div>
                                </div>

                            </div>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-12">
                            <div class="box">
                            <h6><label id="lblFirstName" style="text-decoration: underline;"></label>, since you are in <u>San Jose</u>, you might be intersted in requesting access to:</h6>
                            <div class="row">
                                <div class="col-lg-4 col-md-4 col-sm-12 col-12 mb-3 text-center">
                                    <h6 class="subtitle">Dad's House</h6>
                                    <a href="details">
                                        <div class="box-iot">
                                            Door Lock
                                        </div>
                                    </a>
                                </div>
                                <div class="col-lg-4 col-md-4 col-sm-12 col-12 mb-3 text-center">
                                    <h6 class="subtitle">Mom's House</h6>
                                    <a href="details">
                                        <div class="box-iot">
                                            Door Lock
                                        </div>
                                    </a>
                                </div>
                            </div>
                        </div>
                        </div>
                    </div>
                </div>
                <div class="col-lg-3 col-md-12 col-sm-12 col-12">
                    <div class="box">
                        <h6>Other Devices</h6>
                        <div class="row mb-4">
                            <div class="col-lg-12 col-md-12 col-sm-12 col-12 text-center">
                                <h6 class="subtitle">John's House</h6>
                                <a href="details">
                                    <div class="box-iot">
                                        Door Lock
                                    </div>
                                </a>
                            </div>
                        </div>  
                        <hr/>
                        <div class="row mb-4">
                            <div class="col-lg-12 col-md-12 col-sm-12 col-12 mt-2 text-center">
                                <h6 class="subtitle">John's Workplace</h6>
                                <a href="details">
                                    <div class="box-iot">
                                        Car Lock
                                    </div>
                                </a>
                            </div>
                        </div>  
                        <hr/>
                        <div class="row mb-4">
                            <div class="col-lg-12 col-md-12 col-sm-12 col-12 mt-2 text-center">
                                <h6 class="subtitle">Ben's Music Studio</h6>
                                <a href="details">
                                    <div class="box-iot">
                                        Heater / Ac
                                    </div>
                                </a>
                            </div>
                        </div>  
                    </div>
                </div>
            </div>
        </div>
    </section>
    <script>
        setTimeout(function() {
            document.getElementById("lblName").innerHTML = document.getElementById("Name").value;
            document.getElementById("imgAvatar").src = document.getElementById("Image").value;
            document.getElementById("lblFirstName").innerHTML = document.getElementById("FName").value;
        }, 2000);
    </script>
</asp:Content>

