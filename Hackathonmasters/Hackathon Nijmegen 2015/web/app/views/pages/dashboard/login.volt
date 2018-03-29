<hgroup>
    <h1>HealthPath Dashboard</h1>
</hgroup>
<form>
    <div class="group">
        <input type="email" id="email" value="{{ 'dokter@paradoxis.nl' }}" placeholder="Email"><span class="highlight"></span><span class="bar"></span>
    </div>
    <div class="group">
        <input type="text" id="password" value="{{ 'password' }}" placeholder="Password"><span class="highlight"></span><span class="bar"></span>
    </div>
    <div class="group" style="display: none">
        <div class="alert alert-danger" id="message"></div>
    </div>
    <button type="button" class="button buttonBlue" id="login">Login
        <div class="ripples buttonRipples"><span class="ripplesCircle"></span></div>
    </button>
</form>
<footer>
    <p>Proudly brought to you by <a href="http://www.dalthow.com/" target="_blank">Dalthow</a></p>
</footer>
