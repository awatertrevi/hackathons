<div>
    <style type="text/css">
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
                    <a href="/dashboard/home"><li class="active withripple">Home</li></a>
                    <a href="/dashboard/patients"><li class="withripple">Patients</li></a>
                    <a href="/dashboard/appointments"><li class="withripple">Appointment</li></a>
                </ul>
            </nav>
            <div class="pages col-xs-9">
                <div class="col-xs-10">
                    <div class="well page active" id="home">
                        <h1 class="header">Home</h1>
                        <div class="row text-center">
                        </div>
                    </div>
                </div>
                <div class="col-xs-2">
                    
                </div>
            </div>
        </div>
    </div>
</div>
<script>

</script>