<%@ Page Title="" Language="VB" MasterPageFile="~/MasterPage.master" AutoEventWireup="false" CodeFile="add.aspx.vb" Inherits="add" %>

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
                        <form>
                            <div class="form-group">
                                <label for="device">Device</label><br />
                                <input type="text" id="device" required/>
                            </div>
                            <div class="form-group">
                                <label for="token" required>Access Token</label><br />
                                <input type="text" id="token" />
                            </div>
                            <div class="form-group">
                                <i class="fas fa-plus"></i><label for="range">Add Features</label>
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
                                <h6>Grant Access</h6>
                                <a href="details">
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
</asp:Content>

