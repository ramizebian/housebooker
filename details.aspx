<%@ Page Title="" Language="VB" MasterPageFile="~/MasterPage.master" AutoEventWireup="false" CodeFile="details.aspx.vb" Inherits="details" %>
<%@ Register Src="~/controls/authentication.ascx" TagPrefix="uc1" TagName="authentication" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <uc1:authentication runat="server" ID="authentication" />
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
                        <h6>Heater / Ac</h6>
                        <form>
                            <div class="form-group">
                                <label for="mode">Mode</label><br />
                                <input id="mode" type="radio" name="mode" value="Cold"/> Cold<br />
                                <input type="radio" name="mode" value="Hot"/> Hot    
                            </div>
                            <div class="form-group">
                                <label for="range">Temperature</label>
                                <input id="range" type="range" class="form-control-range"/>
                            </div>
                            <div class="form-group">
                                <!-- Rounded switch -->
                                <label class="switch">
                                    <input type="checkbox">
                                    <span class="slider round"></span>
                                </label>
                            </div>
                            <button type="submit" class="btn btn-primary">Save</button>
                        </form>
                    </div>
                </div>
                <div class="col-lg-3 col-md-12 col-sm-12 col-12">
                    <div class="row">
                        <div class="col-12">
                            <div class="box">
                                <h6>People with Access</h6>
                                <div class="row">
                                    <div class="col-lg-12 col-md-12 col-sm-12 col-12">
                                        <div class="mt-3">
                                            <img alt="" src="images/sample.jpg" class="rounded-circle" width="30" />
                                            <label class="name pl-1">Rami Zebian (Owner)</label>
                                        </div>
                                    </div>
                                </div>  
                                <hr/>
                                <div class="row">
                                    <div class="col-lg-12 col-md-12 col-sm-12 col-12 mt-2">
                                        <div class="mt-3">
                                            <img alt="" src="images/sample.jpg" class="rounded-circle" width="30" />
                                            <label class="name pl-1">Joumana Chehayeb</label>
                                            <label class="float-right"><i class="fas fa-user-times"></i></label>
                                        </div>
                                    </div>
                                </div>  
                                <hr/>
                                <div class="row">
                                    <div class="col-lg-12 col-md-12 col-sm-12 col-12 mt-2 text-left">
                                        <div class="mt-3">
                                            <img alt="" src="images/sample.jpg" class="rounded-circle" width="30" />
                                            <label class="name pl-1">LeLaboDigital</label>
                                            <label class="float-right"><i class="fas fa-user-times"></i></label>
                                        </div>
                                    </div>
                                </div>  
                            </div>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-12">
                            <div class="box">
                                <h6>Grant Access</h6>
                                <a href="details"  data-toggle="modal" data-target="#exampleModal">
                                    <div class="box-iot">
                                        <i class="fas fa-plus"></i> Invite
                                    </div>
                                </a>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </section>
    
    <!-- Modal -->
    <div class="modal fade" id="exampleModal" tabindex="-1" role="dialog" aria-labelledby="exampleModalLabel" aria-hidden="true">
        <div class="modal-dialog" role="document">
            <div class="modal-content">
                <div class="modal-header">
                    <h5 class="modal-title" id="exampleModalLabel">Invite Friends</h5>
                    <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                        <span aria-hidden="true">&times;</span>
                    </button>
                </div>
                <div class="modal-body">
                    <div class="mt-3">
                        <img alt="" src="images/sample.jpg" class="rounded-circle" width="30" />
                        <label class="name pl-1">Friend 1</label><br/>
                        
                        <img alt="" src="images/sample.jpg" class="rounded-circle" width="30" />
                        <label class="name pl-1">Friend 2</label><br/>

                        <img alt="" src="images/sample.jpg" class="rounded-circle" width="30" />
                        <label class="name pl-1">Friend 3</label><br/>
                        
                        <img alt="" src="images/sample.jpg" class="rounded-circle" width="30" />
                        <label class="name pl-1">Friend 4</label><br/>
                        
                        <img alt="" src="images/sample.jpg" class="rounded-circle" width="30" />
                        <label class="name pl-1">Friend 5</label><br/>
                        
                        <img alt="" src="images/sample.jpg" class="rounded-circle" width="30" />
                        <label class="name pl-1">Friend 6</label><br/>
                        
                        <img alt="" src="images/sample.jpg" class="rounded-circle" width="30" />
                        <label class="name pl-1">Friend 7</label><br/>
                        
                        <img alt="" src="images/sample.jpg" class="rounded-circle" width="30" />
                        <label class="name pl-1">Friend 9</label><br/>
                        
                        <img alt="" src="images/sample.jpg" class="rounded-circle" width="30" />
                        <label class="name pl-1">Friend 9</label><br/>
                    </div>
                </div>
                <div class="modal-footer">
                    <button type="button" class="btn btn-secondary" data-dismiss="modal">Close</button>
                    <button type="button" class="btn btn-primary">Invite</button>
                </div>
            </div>
        </div>
    </div>
</asp:Content>

