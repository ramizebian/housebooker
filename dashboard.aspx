<%@ Page Title="" Language="VB" MasterPageFile="~/MasterPage.master" AutoEventWireup="false" CodeFile="dashboard.aspx.vb" Inherits="dashboard" %>
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
                                

                            </div>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-12">
                            <div class="box">
                            <h6><u>Rami</u>, Since you are in <u>San Jose</u>, you might be intersted in requesting access to:</h6>
                            <div class="row">
                                <div class="col-lg-4 col-md-4 col-sm-12 col-12 mb-3 text-center">
                                    <a href="details">
                                        <div class="box-iot">
                                            Car Lock
                                        </div>
                                    </a>
                                    <div class="mt-3">
                                        <img alt="" src="images/sample.jpg" class="rounded-circle" width="30" />
                                        <label class="name pl-1">Dad</label>
                                    </div>
                                </div>
                                <div class="col-lg-4 col-md-4 col-sm-12 col-12 mb-3 text-center">
                                    <a href="details">
                                        <div class="box-iot">
                                            Door Lock
                                        </div>
                                    </a>
                                    <div class="mt-3">
                                        <img alt="" src="images/sample.jpg" class="rounded-circle" width="30" />
                                        <label class="name pl-1">Mom</label>
                                    </div>
                                </div>
                            </div>
                        </div>
                        </div>
                    </div>
                </div>
                <div class="col-lg-3 col-md-12 col-sm-12 col-12">
                    <div class="box">
                        <h6>Other Devices</h6>
                        <div class="row">
                            <div class="col-lg-12 col-md-12 col-sm-12 col-12 text-center">
                                <a href="details">
                                    <div class="box-iot">
                                        Door Lock
                                    </div>
                                </a>
                                <div class="mt-3">
                                    <img alt="" src="images/sample.jpg" class="rounded-circle" width="30" />
                                    <label class="name pl-1">Roni Zebian</label>
                                </div>
                            </div>
                        </div>  
                        <hr/>
                        <div class="row">
                            <div class="col-lg-12 col-md-12 col-sm-12 col-12 mt-2 text-center">
                                <a href="details">
                                    <div class="box-iot">
                                        Car Lock
                                    </div>
                                </a>
                                <div class="mt-3">
                                    <img alt="" src="images/sample.jpg" class="rounded-circle" width="30" />
                                    <label class="name pl-1">Joumana Chehayeb</label>
                                </div>
                            </div>
                        </div>  
                        <hr/>
                        <div class="row">
                            <div class="col-lg-12 col-md-12 col-sm-12 col-12 mt-2 text-center">
                                <a href="details">
                                    <div class="box-iot">
                                        Heater / Ac
                                    </div>
                                </a>
                                <div class="mt-3">
                                    <img alt="" src="images/sample.jpg" class="rounded-circle" width="30" />
                                    <label class="name pl-1">LeLaboDigital</label>
                                </div>
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
            document.getElementById("imgAvatar").src =  document.getElementById("Image").value;
        }, 2000);
    </script>
</asp:Content>

