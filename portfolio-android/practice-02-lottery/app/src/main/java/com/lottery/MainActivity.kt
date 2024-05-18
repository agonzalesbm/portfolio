package com.lottery

import android.content.Intent
import android.os.Bundle
import android.view.View
import android.widget.Button
import android.widget.TextView
import androidx.activity.enableEdgeToEdge
import androidx.appcompat.app.AppCompatActivity
import androidx.core.view.ViewCompat
import androidx.core.view.WindowInsetsCompat
import com.google.android.material.textfield.TextInputLayout

class MainActivity : AppCompatActivity() {
    override fun onCreate(savedInstanceState: Bundle?) {
        super.onCreate(savedInstanceState)
        enableEdgeToEdge()

        setContentView(R.layout.activity_main)

        ViewCompat.setOnApplyWindowInsetsListener(findViewById(R.id.main)) { v, insets ->
            val systemBars = insets.getInsets(WindowInsetsCompat.Type.systemBars())
            v.setPadding(systemBars.left, systemBars.top, systemBars.right, systemBars.bottom)
            insets
        }

        val inputTextName : TextInputLayout = findViewById(R.id.name_input_text)
        var strInputTextName = inputTextName.editText?.getText()
        val lotteryNumbers = generateLotteryNumbers()

        val generateLotteryButton : Button = findViewById(R.id.generate_lottery_button)
        generateLotteryButton.setOnClickListener(View.OnClickListener {
            val intent = Intent(this, ShareLotteryNumberActivity::class.java)
            intent.putExtra("userName", strInputTextName.toString())
            intent.putExtra("lotteryNumbers", lotteryNumbers)
            startActivity(intent)
        })

    }

    private fun generateLotteryNumbers(): String {
        var random: Int = (1..99).random()
        var lotteryResult: String = random.toString()
        for (i in 1..6) {
            random = (1..99).random()
            lotteryResult = lotteryResult.plus(" ").plus(random.toString())
        }
        return lotteryResult
    }
}
