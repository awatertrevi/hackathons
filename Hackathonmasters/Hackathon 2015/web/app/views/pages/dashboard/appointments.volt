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
            color: rgb(51, 51, 70);
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
                    <a href="/dashboard/patients"><li class="withripple">Patients</li></a>
                    <a href="/dashboard/appointments"><li class="active withripple">Appointment</li></a>
                </ul>
            </nav>
            <div class="pages col-xs-9">
                <div class="col-xs-10">
                    <div class="well page active" id="appointments">
                        <h1 class="header">Appointments</h1>
                        <div class="list-group">
                            {% for appointment in appointments %}
                                {% set patient=appointment.getPatients() %}
                                <div class="list-group-item">
                                    <a data-toggle="modal" data-target="#viewModal" data-id="{{ appointment.id|escape }}" data-name="{{ patient.firstname|escape }} {{ patient.lastname|escape }}" class="open-ViewUser">
                                        <div class="row-action-primary">
                                            <i class="mdi-file-folder"></i>
                                        </div>
                                        <div class="row-content">
                                            <div class="least-content">{{ appointment.appointmentDate|escape }}</div>
                                            <h4 class="list-group-item-heading">{{ patient.firstname|escape }} {{ patient.lastname|escape }}</h4>
                                            <p class="list-group-item-text">{{ appointment.subject|escape }}</p>
                                        </div>
                                    </a>
                                </div>

                                {% if not loop.last %}
                                    <div class="list-group-separator"></div>
                                {% endif %}
                            {% endfor %}
                        </div>
                    </div>
                    <div class="well page" id="home">
                    </div>
                </div>
                <div class="col-xs-2">
                    <button class="btn btn-fab btn-material-indigo-900" data-toggle="modal" data-target="#addModal" style="margin-top: 32%;margin-left:30%">
                        <img src="/assets/images/ic_action.png" style="margin-left:-3px;margin-top:-13px"/>
                        <div class="ripple-wrapper"></div></button>
                </div>
            </div>
        </div>

        <div id="addModal" class="modal fade" role="dialog">
            <div class="modal-dialog">
                <div class="modal-content">
                    <div class="modal-header">
                        <button type="button" class="close" data-dismiss="modal">&times;</button>
                        <h4 class="modal-title">Plan Appointment</h4>
                        <div class="separator"></div><br/>
                    </div>
                    <div class="modal-body">
                        <!-- Dynamicly looks through patients while typing the name !-->
                        </h5>Name</h5>
                        <select name="new-name" id="new-name">
                            {% for patient in patients %}
                                <option value="{{ patient.id|escape }}">{{ patient.firstname|escape }} {{ patient.lastname|escape }}</option>
                            {% else %}
                                <option value="">No patients available</option>
                            {% endfor %}
                        </select>
                        <h5>Subject</h5>
                        <textarea class="form-control" id="new-subject"></textarea>
                        <h5>Symptoms</h5>
                        <textarea class="form-control" id="new-symptoms"></textarea>
                        <h5>Date</h5>
                        <input type="date" class="form-control" id="new-date" value="{{ date('Y-m-d') }}">
                        <input type="time" class="form-control" id="new-time" value="{{ date('h:i:s') }}">
                    </div>
                    <div class="modal-footer">
                        <button type="button" class="btn btn-default" data-dismiss="modal">Dismiss</button>
                        <button type="button" class="btn btn-default" id="plan-appointment">Plan</button>
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
                    <h4 class="modal-title">Appointment Overview</h4>
                    <div class="separator"/><br/>
                </div>
                <div class="modal-body">
                    <h5>Name</h5>
                    <label name="nameField" id="nameField">Loading..</label>
                    <h5>Subject</h5>
                    <label name="subject" id="subject">Loading..</label>
                    <h5>Symptoms</h5>
                    <label name="symptoms" id="symptoms">Loading..</label>
                    <h5>Date</h5>
                    <label name="appointmentDate" id="appointmentDate">Loading..</label>
                </div>
                <div class="modal-footer">
                    <button type="button" class="btn btn-default" id="postAppointmentButton">Post Appointment Log</button>
                    <button type="button" class="btn btn-default" data-dismiss="modal">Close</button>
                </div>
            </div>
        </div>
    </div>
</div>

<div id="postAppointmentModal" class="modal fade" role="dialog">
    <div class="modal-dialog">
        <div class="modal-content">
            <div class="modal-header">
                <button type="button" class="close" data-dismiss="modal">&times;</button>
                <h4 class="modal-title">Post Appointment</h4>
                <div class="separator"/><br/>
            </div>
            <div class="modal-body">
                <h5>Prescription</h5>
                <input type="text" class="form-control" name="prescription" id="prescription">

                <h4>Questions</h4>
                <div class="col-xs-6">
                    <input class="form-control" type="text" id="question-1" placeholder="Question 1" />
                </div>
                <div class="col-xs-6">
                    <input class="form-control" type="text" id="answer-1" placeholder="Answer 1" />
                </div>
                <div class="col-xs-6">
                    <input class="form-control" type="text" id="question-2" placeholder="Question 2" />
                </div>
                <div class="col-xs-6">
                    <input class="form-control" type="text" id="answer-2" placeholder="Answer 2" />
                </div>
                <div class="col-xs-6">
                    <input class="form-control" type="text" id="question-3" placeholder="Question 3" />
                </div>
                <div class="col-xs-6">
                    <input class="form-control" type="text" id="answer-3" placeholder="Answer 3" />
                </div>

            </div>
            <div class="modal-footer">
                <button type="button" class="btn btn-default" id="preAppointmentButton">Pre Appointment</button>
                <button type="button" class="btn btn-default" id="save-questions">Save</button>
                <button type="button" class="btn btn-default" data-dismiss="modal">Close</button>
            </div>
        </div>
    </div>
</div>