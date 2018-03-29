<div>
    <style>
        * {
            box-sizing: border-box;
        }
        .header-panel {
            background-color: #5c6bc0;
            height: 144px;
            position: relative;
            z-index: 3;
        }
        .header-panel div {
            position: relative;
            height: 100%;
        }
        .header-panel h1 {
            color: #FFF;
            font-size: 20px;
            font-weight: 400;
            position: absolute;
            bottom: 10px;
            padding-left: 35px;
        }

        .menu {
            overflow: auto;
            padding: 0;
        }
        .menu, .menu * {
            -webkit-user-select: none;
            -moz-user-select: none;
            user-select: none;
        }
        .menu ul {
            padding: 0;
            margin: 7px 0;
        }
        .menu ul li {
            list-style: none;
            padding: 20px 0 20px 50px;
            font-size: 15px;
            font-weight: normal;
            cursor: pointer;
        }
        .menu ul li.active {
            background-color: #dedede;
            position: relative;
        }
        .menu ul li a {
            color: rgb(51, 51, 51);
            text-decoration: none;
        }

        .pages {
            position: absolute;
            top: 0;
            right: 0;
            z-index: 4;
            padding: 0;
            overflow: auto;
        }
        .pages > div {
            padding: 0 5px;
            padding-top: 64px;
        }

        .pages .header {
            color: rgb(82, 101, 162);
            font-size: 24px;
            font-weight: normal;
            margin-top: 5px;
            margin-bottom: 60px;
            letter-spacing: 1.20000004768372px;
        }

        .page {
            transform: translateY(1080px);
            transition: transform 0 linear;
            display: none;
            opacity: 0;
            font-size: 16px;
        }

        .page.active {
            transform: translateY(0px);
            transition: all 0.3s ease-out;
            display: block;
            opacity: 1;
        }

    </style>
    <div class="header-panel shadow-z-2">
        <div class="container-fluid">
            <div class="row">
                <div class="col-xs-3">
                    <h1>HealthPath Dashboard</h1>
                </div>
            </div>
        </div>
    </div>
    <div class="container-fluid main">
        <div class="row">
            <nav class="col-xs-3 menu">
                <ul>
                    <a href="/dashboard/home"><li class="withripple">Home</li></a>
                    <a href="/dashboard/patients"><li class="active withripple">Patients</li></a>
                    <a href="/dashboard/appointments"><li class="withripple">Appointment</li></a>
                </ul>
            </nav>
            <div class="pages col-xs-9">
                <div class="col-xs-10">
                    <div class="well page active" id="home">
                        <h1 class="header">Patients</h1>
                        <div class="list-group">
                            {% for patient in patients %}
                                <a data-toggle="modal" data-target="#viewModal" data-id="{{ patient.id|escape }}" class="open-ViewUser">
                                    <div class="list-group-item">
                                        <div class="row-action-primary">
                                            <i class="mdi-file-folder"></i>
                                        </div>

                                        <div class="row-content">
                                            <div class="least-content"></div>
                                            <h4 class="list-group-item-heading">{{ patient.lastname|escape }}</h4>
                                            <p class="list-group-item-text">{{ patient.firstname|escape }}</p>
                                        </div>
                                    </div>
                                </a>
                                {% if not loop.last %}
                                    <div class="list-group-separator"></div>
                                {% endif %}
                            {% endfor %}

                        </div>

                    </div>
                </div>
                <div class="col-xs-2">
                                    <button class="btn btn-fab btn-material-indigo-900" data-toggle="modal" data-target="#addModal" style="margin-top: 32%;margin-left:30%"><img src="/assets/images/icon-action.png" style="margin-left:-3px;margin-top:-13px"/><div class="ripple-wrapper"></div></button>
                                </div>
            </div>
        </div>
    </div>
</div>

<div id="viewModal" class="modal fade" role="dialog">
    <div class="modal-dialog">
        <div class="modal-content">
            <div class="modal-header">
                <button type="button" class="close" data-dismiss="modal">&times;</button>
                <h4 class="modal-title">Patient Overview</h4>
                <div class="separator"/><br/>
            </div>
            <div class="modal-body">
                <h5>Name</h5>
                <label><span id="firstname">Loading..</span> <span id="lastname">Loading..</span></label>
                <h5>Date of birth</h5>
                <label id="birthday">Loading..</label>
                <h5>Phone Number</h5>
                <label id="phone">Loading..</label>
                <h5>E-Mail</h5>
                <label id="email">Loading..</label>
                <br/>
                <h5>ZIP Code</h5>
                <label id="zip">Loading..</label>
                <h5>House Number</h5>
                <label id="house">Loading..</label>
                <br/>
                <h5>Insurance</h5>
                <label id="insurance">Loading..</label>
                <h5>Risk</h5>
                <label id="risk">Loading..</label>
            </div>
            <div class="modal-footer">
                <button type="button" class="btn btn-default" data-dismiss="modal">Close</button>
            </div>
        </div>
    </div>
</div>
</div>

<div id="addModal" class="modal fade" role="dialog" style="overflow-y: auto;">
    <div class="modal-dialog">
        <div class="modal-content">
            <div class="modal-header">
                <button type="button" class="close" data-dismiss="modal">&times;</button>
                <h4 class="modal-title">Add Patient</h4>
                <div class="separator"/><br/>
            </div>
                <div class="modal-body">
                    <h5>Firstname</h5>
                    <input type="text" class="form-control" name="firstname">
                    <h5>Lastname</h5>
                    <input type="text" class="form-control" name="lastname">
                    <h5>Email</h5>
                    <input type="text" class="form-control" name="email">
                    <h5>Password</h5>
                    <input type="text" class="form-control" name="password">
                    <h5>Phone</h5>
                    <input type="text" class="form-control" name="phone">
                    <h5>BDay</h5>
                    <input type="text" class="form-control" name="bday">
                    <h5>Insurance</h5>
                    <input type="text" class="form-control" name="insurance">
                    <h5>Risk</h5>
                    <input type="text" class="form-control" name="risk">
                    <h5>Zip</h5>
                    <input type="text" class="form-control" name="zip">
                    <h5>House</h5>
                    <input type="text" class="form-control" name="house">
                    <button type="button" class="btn btn-default" id="add-user">Add</button>
                </div>
            </div>
        </div>
    </div>
</div>
